using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var application = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (application == null)
            {
                return new PDFExportResponse(null, "application/pdf", $"Application with ID {request.Id} not found.");
            }

            var result = _mapper.Map<ApplicationResponse>(application);
            string htmlTemplate = GetHtmlTemplate();
            string mergedHtml = MergeDataIntoHtml(htmlTemplate, result);

            // Generate the PDF from the merged HTML
            byte[] pdfBytes = GeneratePdfFromHtml(mergedHtml);

            return new PDFExportResponse(pdfBytes, "application/pdf", $"{request.Id}_Application.pdf");
        }

        private string MergeDataIntoHtml(string htmlTemplate, ApplicationResponse result)
        {
            htmlTemplate = htmlTemplate.Replace("{{Id}}", result.Id.ToString());
            htmlTemplate = htmlTemplate.Replace("{{Inn}}", result.Inn);

            return htmlTemplate;
        }

        private Domain.Entities.Application FilterIfApplicationExists(int id)
        {
            return _dbContext.Applications.SingleOrDefault(a => a.Id == id);
        }

        private byte[] GeneratePdfFromHtml(string html)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();

                // Convert the merged HTML to a PDF using iTextSharp
                using (TextReader reader = new StringReader(html))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);
                }

                document.Close();

                return ms.ToArray();
            }
        }
        private string GetHtmlTemplate()
        {
            return
            @"<html>
<head></head>
<body>
  <div class=""doc_one"" style=""
    line-height: 22px;
    font-family: sans-serif;
    margin-left: 220px;
    margin-right: 220px;
    background-color: rgb(248, 248, 248);
    min-height: 1400px;
  "">
    <div class=""body"" style=""padding-left: 10%; padding-right: 10%"">
      <div class=""header"" style=""margin-left: 50%; padding-top: 60px"">
        <div style=""display: flex; justify-content: space-around"">
          <div style=""font-size: 17px"">Кимга:</div>
          <div style=""
              margin-left: 40px;
              text-align: center;
              font-weight: bold;
              font-size: 17px;
            "">
            Ўзбекистон Республикаси Савдо-саноат палатаси
            <span style=""background-color: yellow; padding: 3px"">
              Тошкент вилояти</span>
            ҳудудий бошқармасига
          </div>
        </div>
      </div>
      <div class=""header"" style=""margin-left: 50%; padding-top: 60px"">
        <div style=""display: flex"">
          <div style=""font-size: 17px"">Кимдан:</div>
          <div style=""
              margin-left: 40px;
              text-align: center;
              font-size: 17px;
            "">
            <span style=""font-weight: bold"">Палата аъзоси</span>
            <br>________________________ <br>Манзил: __________ вилояти,<br>___________
            тумани (шаҳри) <br>_______________ МФЙ;<br>Банк ва ҳисоб рақами:
            _______________ <br>______________________________ <br>СТИР:
            ______________
          </div>
        </div>
      </div>
      <div class=""title"" style=""
          text-align: center;
          margin-top: 60px;
          font-weight: bold;
          color: blue;
          font-size: 18px;
        "">
        Палата аъзоси манфаатида судга даъво аризаси киритиш тўғрисида
      </div>
      <div class=""ariza-box"">
        <div class=""ariza"" style=""
            text-align: center;
            margin-top: 10px;
            font-weight: bold;
            color: red;
            font-size: 18px;
          "">
          АРИЗА
        </div>
      </div>
      <div class=""left-righit"" style=""display: flex; justify-content: space-between"">
        <div class=""left"" style=""font-weight: bold"">№___________</div>
        <div class=""right"" style=""font-weight: bold"">Сана:___________</div>
      </div>
      <div class=""info"" style=""padding-top: 40px; line-height: 30px"">
        <div>
          “Ўзбекистон Республикаси Савдо-саноат палатаси тўғрисида”ги Қонун ва
        </div>
        <div>Палата аъзоси билан тузилган аъзолик шартномасига асосан,</div>
        <div style=""display: flex; text-align: center"">
          <div>
            _______________________________________
            <div><div>(Палата аъзосининг номи)</div></div>
          </div>
          манфаатида жавобгар(лар)
        </div>
        <br>
      </div>
      <div style=""display: flex; text-align: center"">
        <div>
          _______________________________________
          <div><div>(Корхона номи ёки ж/ш исм-шарифи)</div></div>
        </div>
        га нисбатан судга
      </div>
      <div style=""display: flex; text-align: center"">
        <div>
          _______________________________________
          <div><div>(даъво предмети)</div></div>
        </div>
        тўғрисида даъво аризаси киритишда
      </div>
      <div>амалий ёрдам беришингизни сўрайман.</div>
      <div style=""padding-left: 8%; padding-right: 8%; padding-top: 40px"">
        <div style=""font-weight: bold"">Жавобгар(лар) тўғрисида маълумот:</div>
        <div style=""display: flex"">
          <div>
            ______________________________________________________________________________
            <div>
              <div style=""text-align: center"">
                (Корхона номи ёки ж/ш исм-шарифи)
              </div>
            </div>
          </div>
        </div>
        <div style=""display: flex; justify-content: space-around"">
          <div style=""font-weight: bold; padding-top: 7px"">Манзил:</div>
          <div style=""margin-left: 5px"">
            ______________________ вилоят, ______________________________
            туман (шаҳар)
          </div>
        </div>
        <div style=""display: flex; justify-content: space-around"">
          <div></div>
          <div style=""margin-left: 60px"">
            ____________ МФЙ, __________ кўчаси, ___ уй.
          </div>
        </div>
        <div>
          <div style=""display: flex"">
            <div style=""font-weight: bold; padding-top: 7px"">Телефон:</div>
            <div style=""margin-left: 5px"">
              __________________________________________________________________
            </div>
          </div>
        </div>
        <div>
          <div style=""display: flex"">
            <div style=""font-weight: bold; padding-top: 7px"">
              Банк ҳисоб рақами:
            </div>
            <div style=""margin-left: 5px"">______________________</div>
          </div>
        </div>
        <div>
          <div style=""display: flex"">
            СТИР: _____________; ЖШШИР: _____________________ (агар жисмоний
            бўлса)
          </div>
        </div>
        <div>
          <div style=""display: flex; font-weight: bold; padding-top: 7px"">
            Даъво суммаси жами: _____________ (сўм, АҚШ доллари, Евро, Рубль)
            (даъво
          </div>
        </div>
        <div>
          <div>
            <div style=""display: flex"">
              предметидан келиб чиқиб мавжуд бўлганда):
            </div>
            <div>
              <div style=""display: flex; font-weight: bold; padding-top: 7px"">
                Шундан:
              </div>
            </div>
          </div>
          <div>
            <div style=""display: flex"">
              асоссий қарздорлик - _____________ (сўм, АҚШ доллари, Евро,
              Рубль);
            </div>
            <div style=""display: flex"">
              ҳисобланган пеня - _____________ (сўм, АҚШ доллари, Евро,
              Рубль);
            </div>
            <div style=""display: flex"">
              жарима - _____________ (сўм, АҚШ доллари, Евро, Рубль);
            </div>
            <div style=""display: flex"">
              фоиз - _____________ (сўм, АҚШ доллари, Евро, Рубль).
            </div>
            <div style=""display: flex; padding-top: 30px"">
              <div style=""padding-top: 14px"">
                Илова: электрон шаклда мавжуд
              </div>
              <div style=""margin-left: 20px"">
                <img style=""padding-top: 10px"" src=""data:image/jpeg;base64,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"">
              </div>
            </div>
            <div style=""
                display: flex;
                justify-content: space-between;
                padding-top: 40px;
              "">
              <div style=""font-size: 17px; font-weight: bold"">
                Корхона раҳбарининг Ф.И.Ш.
              </div>
              <div style=""font-size: 17px; font-weight: bold"">
                (электрон имзо)
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</body>
</html>";
        }
    }

}


