using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Reports
{
    public record GetResponsiblePersonByIdPDFQuery(int Id) : IRequest<PDFExportResponse>;

    public class GetResponsiblePersonByIdQueryHandler : IRequestHandler<GetResponsiblePersonByIdPDFQuery, PDFExportResponse> // Change the response type to PDFExportResponse
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetResponsiblePersonByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PDFExportResponse> Handle(GetResponsiblePersonByIdPDFQuery request, CancellationToken cancellationToken)
        {
            var responsiblePerson = FilterIfResponsiblePersonExists(request.Id);

            var result = _mapper.Map<ResponsiblePersonResponse>(responsiblePerson);

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                document.SetMargins(20, 20, 40, 40);
                document.SetPageSize(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                HeaderFooterHelper headerFooter = new HeaderFooterHelper();
                writer.PageEvent = headerFooter;

                document.Open();

                PdfPTable table = new PdfPTable(2); // Two columns for the ID and value

                table.SetWidths(new float[] { 1f, 3f }); // Adjust the column widths accordingly

                table.SpacingBefore = 10;
                table.SpacingAfter = 10;

                // Add the ID and value as cell values
                table.AddCell("ID");
                table.AddCell(result.Id.ToString());
                table.CompleteRow();

                table.AddCell("Ordinal Number");
                table.AddCell(result.OrdinalNumber);
                table.CompleteRow();

                table.AddCell("Inn");
                table.AddCell(result.Inn);
                table.CompleteRow();

                table.AddCell("Full Name");
                table.AddCell(result.FullName);
                table.CompleteRow();

                table.AddCell("Address");
                table.AddCell(result.Address);
                table.CompleteRow();

                table.AddCell("Phone Number");
                table.AddCell(result.PhoneNumber);
                table.CompleteRow();

                table.AddCell("Application ID");
                table.AddCell(result.ApplicationId.ToString());
                table.CompleteRow();

                table.AddCell("Type Of Responsible Person ID");
                table.AddCell(result.TypeOfResponsiblePersonId.ToString());
                table.CompleteRow();

                table.AddCell("Created By");
                table.AddCell(result.CreatedBy);
                table.CompleteRow();

                table.AddCell("Created Date");
                table.AddCell(result.CreatedDate.ToString());
                table.CompleteRow();

                table.AddCell("Modify By");
                table.AddCell(result.ModifyBy);
                table.CompleteRow();

                table.AddCell("Modify Date");
                table.AddCell(result.ModifyDate.ToString());
                table.CompleteRow();

                document.Add(table);
                document.Close();

                return await Task.FromResult(new PDFExportResponse(ms.ToArray(), "application/pdf", $"{request.Id}_ResponsiblePerson.pdf"));
            }
        }

        private ClaimApplication.Domain.Entities.ResponsiblePerson FilterIfResponsiblePersonExists(int id)
            => _dbContext.ResponsiblePeople.Find(id)
                ?? throw new NotFoundException("There is no Responsible Person with this Id.");
    }

    public class HeaderFooterHelper : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footerTable = new PdfPTable(1);
            footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
            footerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            footerTable.AddCell(new Phrase($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}", new Font(Font.FontFamily.HELVETICA, 8)));

            footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
        }
    }

    public record PDFExportResponse(byte[] FileContents, string Options, string FileName);
}
