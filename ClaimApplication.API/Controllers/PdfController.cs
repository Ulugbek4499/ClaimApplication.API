using System.Dynamic;
using System.Web.Http;
using ClaimApplication.Application.Commons.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using RazorLight;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    public class PdfController : ApiController
    {
        private readonly IApplicationDbContext _dbContext;

        public PdfController(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/pdf/{applicationId}")]
        public async Task<IHttpActionResult> GeneratePdf(int applicationId)
        {
            try
            {
                // Get the application data from the database
                var application = await _dbContext.Applications.FindAsync(applicationId);
                if (application == null)
                    return NotFound();

                // Create a dynamic model from the Application object
                dynamic model = new ExpandoObject();
                model.Inn = application.Inn;
                model.NameOfBussiness = application.NameOfBussiness;
                model.AppealNumber = application.AppealNumber;
                model.AppealDate = application.AppealDate;
                // Add other properties as needed...

                // Read the Razor template file
                string templatePath = "ClaimApplicationTemplate.cshtml";
                string htmlContent = await RenderRazorTemplate(templatePath, model);

                // Generate the PDF from the HTML content
                byte[] pdfBytes = GeneratePdfFromHtml(htmlContent);

                // Set the content type and headers for the response
                var response = new HttpResponseMessage();
                response.Content = new ByteArrayContent(pdfBytes);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "ClaimApplication.pdf";
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during PDF generation
                return InternalServerError(ex);
            }
        }

        private async Task<string> RenderRazorTemplate(string templatePath, dynamic model)
        {
            // Read the Razor template file from wwwroot
            string templateContent = File.ReadAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", templatePath));

            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();

            // Use dynamic as the model type for the Razor template
            string renderedContent = await engine.CompileRenderStringAsync(templateContent, templatePath, model);

            return renderedContent;
        }

        private byte[] GeneratePdfFromHtml(string htmlContent)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                using (StringReader stringReader = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                }

                document.Close();

                return memoryStream.ToArray();
            }
        }
    }
    public class ApplicationViewModel
    {
        public string Inn { get; set; }
        public string NameOfBussiness { get; set; }
        public int AppealNumber { get; set; }
        public DateTime AppealDate { get; set; }
        // Add other properties as needed...
    }
}
