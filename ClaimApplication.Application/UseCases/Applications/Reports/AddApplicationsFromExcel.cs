using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClaimApplication.Application.UseCases.Applications.Reports
{
    public record AddApplicationsFromExcel(IFormFile ExcelFile) : IRequest<List<ApplicationResponse>>;

    public class AddApplicationsFromExcelHandler : IRequestHandler<AddApplicationsFromExcel, List<ApplicationResponse>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddApplicationsFromExcelHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ApplicationResponse>> Handle(AddApplicationsFromExcel request, CancellationToken cancellationToken)
        {
            if (request.ExcelFile == null || request.ExcelFile.Length == 0)
                throw new ArgumentNullException("File", "file is empty or null");

            var file = request.ExcelFile;
            List<Domain.Entities.Application> result = new();
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms, cancellationToken);
                using (var wb = new XLWorkbook(ms))
                {
                    var sheet1 = wb.Worksheet(1);
                    int startRow = 2;
                    for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
                    {
                        var Application = new Domain.Entities.Application()
                        {
                            Inn = sheet1.Cell(row, 1).GetString(),
                            NameOfBussiness = sheet1.Cell(row, 2).GetString(),
                            AppealNumber = int.Parse(sheet1.Cell(row, 3).GetString()),
                            AppealDate = DateTime.Parse(sheet1.Cell(row, 4).GetString()),
                            MembershipAgreementNumber = sheet1.Cell(row, 5).GetString(),
                            MembershipAgreementDate = DateTime.Parse(sheet1.Cell(row, 6).GetString()),
                            CertificateNumber = sheet1.Cell(row, 7).GetString(),
                            CertificateGivenDate = DateTime.Parse(sheet1.Cell(row, 8).GetString()),
                            PreviousAppeal = sheet1.Cell(row, 9).GetString(),
                            AppealText = sheet1.Cell(row, 10).GetString(),
                            TotalClaimAmount = decimal.Parse(sheet1.Cell(row, 11).GetString()),
                            MainDebt = decimal.Parse(sheet1.Cell(row, 12).GetString()),
                            CalculatedLateCharges = decimal.Parse(sheet1.Cell(row, 13).GetString()),
                            AmountOfFine = decimal.Parse(sheet1.Cell(row, 14).GetString()),
                            Percentage = decimal.Parse(sheet1.Cell(row, 15).GetString()),
                            AppealPredmetId = int.Parse(sheet1.Cell(row, 16).GetString()),
                            AppealTypeId = int.Parse(sheet1.Cell(row, 17).GetString())
                        };

                        result.Add(Application);
                    }
                }
            }

            await _context.Applications.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<ApplicationResponse>>(result);
        }
    }
}
