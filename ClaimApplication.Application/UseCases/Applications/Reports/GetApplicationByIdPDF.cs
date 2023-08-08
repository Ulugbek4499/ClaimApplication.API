using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Reports
{
    public record GetApplicationByIdPDFQuery(int Id) : IRequest<PDFExportResponse>;

    public class GetApplicationByIdPDFQueryHandler : IRequestHandler<GetApplicationByIdPDFQuery, PDFExportResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetApplicationByIdPDFQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PDFExportResponse> Handle(GetApplicationByIdPDFQuery request, CancellationToken cancellationToken)
        {
            var application = FilterIfApplicationExists(request.Id);

            var result = _mapper.Map<ApplicationResponse>(application);

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                document.SetMargins(20, 20, 40, 40);
                document.SetPageSize(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                HeaderFooterHelper headerFooter = new HeaderFooterHelper();
                writer.PageEvent = headerFooter;

                document.Open();

                PdfPTable table = new PdfPTable(2);

                table.SetWidths(new float[] { 2f, 3f });

                table.SpacingBefore = 10;
                table.SpacingAfter = 10;

                table.AddCell("ID");
                table.AddCell(result.Id.ToString());
                table.CompleteRow();

                table.AddCell("Inn");
                table.AddCell(result.Inn);
                table.CompleteRow();

                table.AddCell("Name Of Bussiness");
                table.AddCell(result.NameOfBussiness);
                table.CompleteRow();

                table.AddCell("Appeal Number");
                table.AddCell(result.AppealNumber.ToString());
                table.CompleteRow();

                table.AddCell("Appeal Date");
                table.AddCell(result.AppealDate.ToString());
                table.CompleteRow();

                table.AddCell("Membership Agreement Number");
                table.AddCell(result.MembershipAgreementNumber);
                table.CompleteRow();

                table.AddCell("Membership Agreement Date");
                table.AddCell(result.MembershipAgreementDate.ToString());
                table.CompleteRow();

                table.AddCell("Certificate Number");
                table.AddCell(result.CertificateNumber);
                table.CompleteRow();

                table.AddCell("Certificate Given Date");
                table.AddCell(result.CertificateGivenDate.ToString());
                table.CompleteRow();

                table.AddCell("Previous Appeal");
                table.AddCell(result.PreviousAppeal);
                table.CompleteRow();

                table.AddCell("Appeal Text");
                table.AddCell(result.AppealText);
                table.CompleteRow();

                table.AddCell("Total Claim Amount");
                table.AddCell(result.TotalClaimAmount.ToString());
                table.CompleteRow();

                table.AddCell("Main Debt");
                table.AddCell(result.MainDebt.ToString());
                table.CompleteRow();

                table.AddCell("Calculated Late Charges");
                table.AddCell(result.CalculatedLateCharges.ToString());
                table.CompleteRow();

                table.AddCell("Amount Of Fine");
                table.AddCell(result.AmountOfFine.ToString());
                table.CompleteRow();

                table.AddCell("Percentage");
                table.AddCell(result.Percentage.ToString());
                table.CompleteRow();

                table.AddCell("Appeal Predmet Id");
                table.AddCell(result.AppealPredmetId.ToString());
                table.CompleteRow();

                table.AddCell("Appeal Type Id");
                table.AddCell(result.AppealTypeId.ToString());
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

                return await Task.FromResult(new PDFExportResponse(ms.ToArray(), "application/pdf", $"{request.Id}_Application.pdf"));
            }
        }

        private Domain.Entities.Application FilterIfApplicationExists(int id)
            => _dbContext.Applications.Find(id) ?? throw new NotFoundException("There is no Application with this ID.");
    }

}
