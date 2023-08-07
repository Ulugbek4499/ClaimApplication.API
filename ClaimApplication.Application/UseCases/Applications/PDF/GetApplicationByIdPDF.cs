using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.PDF;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Reports;
using iText.Html2pdf.Resolver.Font;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using MediatR;
using iText.Kernel.Geom;
using iText.Layout;

namespace ClaimApplication.Application.UseCases.Applications.Reports
{
    public record GetApplicationPDFQuery(int Id) : IRequest<PDFExportResponse>;

    public class GetApplicationPDFQueryHandler : IRequestHandler<GetApplicationPDFQuery, PDFExportResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetApplicationPDFQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PDFExportResponse> Handle(GetApplicationPDFQuery request, CancellationToken cancellationToken)
        {
            byte[] ByteArray = GetPdfTemplate(request.Id);

            return new PDFExportResponse(ByteArray, "application/pdf", "ClaimApplication.html");
        }

        public byte[] GetPdfTemplate(int applicationId)
        {
            var application = FilterIfApplicationExists(applicationId);

            var result = _mapper.Map<ApplicationResponse>(application);

            return ReturnReadyPdf(GetHtmlTemplate(result));
        }

        private Domain.Entities.Application FilterIfApplicationExists(int id)
               => _dbContext.Applications.Find(id) ?? throw new NotFoundException("There is no Application with this ID.");

        public string GetHtmlTemplate(ApplicationResponse application)
        {
            var inn = application.Inn;
            var nameOfBussiness = application.NameOfBussiness;
            var appealNumber = application.AppealNumber;
            var appealDate = application.AppealDate;
            var membershipAgreementNumber = application.MembershipAgreementNumber;
            var membershipAgreementDate = application.MembershipAgreementDate;
            var certificateNumber = application.CertificateNumber;
            var certificateGivenDate = application.CertificateGivenDate;
            var previousAppeal = application.PreviousAppeal;
            var appealText = application.AppealText;
            var totalClaimAmount = application.TotalClaimAmount;
            var mainDebt = application.MainDebt;
            var calculatedLateCharges = application.CalculatedLateCharges;
            var amountOfFine = application.AmountOfFine;
            var percentage = application.Percentage;
            var appealPredmetId = application.AppealPredmetId;
            var appealTypeId = application.AppealTypeId;

            Dictionary<string, string> data
                = new Dictionary<string, string>()
                {
               {"${INN}", inn},
               {"${ContractorRegion}",nameOfBussiness},
               {"${ContractorDistrict}",appealNumber.ToString()},
               {"${ContractorName}",appealDate.ToString()},
               {"${ContractorDirector}",membershipAgreementNumber},
               {"${ContractorInn}",membershipAgreementDate.ToString()},
               {"${ContractorBankCode}",certificateNumber},
               {"${ContractorBankName}",certificateGivenDate.ToString()},
               {"${ContractorSettlementAccount}",previousAppeal},
               {"${OrgRegion}",appealText},
               {"${OrgDistrict}",totalClaimAmount.ToString()},
               {"${OrgAdress}",mainDebt.ToString()},
               {"${OrgBankSettlementAccount}",calculatedLateCharges.ToString()},
               {"${OrgBankCode}",amountOfFine.ToString()},
               {"${OrgBankName}",percentage.ToString()},
               {"${OrgInn}",appealPredmetId.ToString()},
               {"${ContractorPosition1}",appealTypeId.ToString()}
                };

            string fileName = "ClaimApplication.html";


            return ReturnReadyHtmlAsString(filename: fileName,
                                                      data: data);
        }

        public byte[] ReturnReadyPdf(string htmlString)
        {
            var workStream = new MemoryStream();

            using (var pdfWriter = new PdfWriter(workStream))
            {
                pdfWriter.SetCloseStream(false);

                var pdfDocument = new PdfDocument(pdfWriter);

                var documentConvertProperties = new ConverterProperties();

                using (var document = HtmlConverter.ConvertToDocument(htmlString, pdfDocument, documentConvertProperties))
                {
                    document.SetBottomMargin(0);
                    document.SetTopMargin(0);
                    document.Flush();
                }
            }

            workStream.Position = 0;
            return workStream.ToArray();
        }

        public string ReturnReadyHtmlAsString(string filename, Dictionary<string, string> data)
        {
            var html = GetFileFromStorage(filename);

            string htmlStream = html.ReadAsString();

            if (data != null)
                foreach (var (key, value) in data)
                    if (htmlStream.Contains(key))
                        htmlStream = htmlStream.Replace(key, value);
                    else
                        htmlStream = htmlStream.Replace(key, "");

            return htmlStream;
        }

        private FileInfo GetFileFromStorage(string filename)
        {
            string filePath = "D:\\PDP\\COMPANIES\\ClaimApplication.API\\ClaimApplication.API\\wwwroot\\html\\ClaimApplication.html"; //System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "html", filename);
            FileInfo fileInfo = new FileInfo(filePath);

            return fileInfo;
        }





        public static string HtmlToPdf(string htmlFilePath, bool isLandscapeOrientation = false)
        {
            string tempPath = "html/";
            var fileInfo = new FileInfo(htmlFilePath);
            string pdfFileName = $"{tempPath}{Guid.NewGuid()}.pdf";

            using (var htmlStream = File.Open(htmlFilePath, FileMode.Open, FileAccess.Read))
            {
                var writerProperties = new WriterProperties();

                using (var pdfWriter = new PdfWriter(pdfFileName, writerProperties))
                {
                    var pdfDocument = new PdfDocument(pdfWriter);
                    var pageSize = PageSize.A4;

                    if (isLandscapeOrientation)
                        pageSize = pageSize.Rotate();

                    var documentConvertProperties = new ConverterProperties();

                    DefaultFontProvider fontProvider = new DefaultFontProvider(true, true, true, "Times New Roman");

                    string fontDir = System.IO.Path.Combine("appdata", "staticfiles", "fonts");

                    if (Directory.Exists(fontDir))
                        fontProvider.AddDirectory(fontDir);

                    documentConvertProperties.SetFontProvider(fontProvider);

                    pdfDocument.SetDefaultPageSize(pageSize);

                    #region Convert to html
                    IList<IElement> elements = HtmlConverter.ConvertToElements(htmlStream, documentConvertProperties);
                    pdfDocument.SetTagged();

                    Document doc = new Document(pdfDocument, pageSize);

                    foreach (IElement element in elements)
                        doc.Add((IBlockElement)element);
                    doc.Close();
                    #endregion
                }
            }

            return pdfFileName;
        }

        public static void AddTextToPdf(Stream source, Stream destination, PdfText text)
        {
            AddTextToPdf(
                source,
                destination,
                text == null ? null : new PdfText[] { text });
        }

        public static void AddTextToPdf(Stream source, Stream destination, IEnumerable<PdfText> texts)
        {
            using (var pdfReader = new PdfReader(source))
            {
                using (var ms = new MemoryStream())
                {
                    using (var pdfWriter = new PdfWriter(ms))
                    {
                        var pdfDocument = new PdfDocument(pdfReader, pdfWriter);
                        var pageSize = PageSize.A4;
                        pdfDocument.SetDefaultPageSize(pageSize);
                        pdfDocument.SetTagged();
                        Document doc = new Document(pdfDocument);

                        if (texts != null)
                            foreach (var text in texts)
                                doc.ShowTextAligned(text.Value, text.X, text.Y, text.Page, text.Alignment, text.VerticalAlignment, text.RadAngle);

                        doc.Close();
                        var bytes = ms.ToArray();
                        destination.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
