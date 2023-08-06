using System.Data;
using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Domain.Entities;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimApplication.Application.UseCases.Applications.Reports
{
    public class GetApplicationsExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetApplicationsExcelHandler : IRequestHandler<GetApplicationsExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetApplicationsExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetApplicationsExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetApplicationsAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "Applications");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 18;
                excelSheet.Column(2).Width = 18;
                excelSheet.Column(3).Width = 18;
                excelSheet.Column(4).Width = 18;
                excelSheet.Column(5).Width = 18;
                excelSheet.Column(6).Width = 18;
                excelSheet.Column(7).Width = 18;
                excelSheet.Column(8).Width = 18;
                excelSheet.Column(9).Width = 18;
                excelSheet.Column(10).Width = 18;
                excelSheet.Column(11).Width = 18;
                excelSheet.Column(12).Width = 18;
                excelSheet.Column(13).Width = 18;
                excelSheet.Column(14).Width = 18;
                excelSheet.Column(15).Width = 18;
                excelSheet.Column(16).Width = 18;
                excelSheet.Column(17).Width = 18;
                excelSheet.Column(18).Width = 18;
                excelSheet.Column(19).Width = 18;
                excelSheet.Column(20).Width = 18;
                excelSheet.Column(21).Width = 18;
                excelSheet.Column(22).Width = 18;
                excelSheet.Column(23).Width = 18;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetApplicationsAsync(CancellationToken cancellationToken = default)
        {
            var AllApplications = await _context.Applications.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Id", typeof(int));
            excelDataTable.Columns.Add("INN", typeof(string));
            excelDataTable.Columns.Add("Name Of Bussiness", typeof(string));
            excelDataTable.Columns.Add("AppealNumber", typeof(int));
            excelDataTable.Columns.Add("Appeal Date", typeof(DateTime));
            excelDataTable.Columns.Add("Membership Agreement Number", typeof(string));
            excelDataTable.Columns.Add("Membership Agreement Date", typeof(DateTime));
            excelDataTable.Columns.Add("CertificateNumber", typeof(string));
            excelDataTable.Columns.Add("CertificateGiven Date", typeof(DateTime));
            excelDataTable.Columns.Add("PreviousAppeal", typeof(string));
            excelDataTable.Columns.Add("AppealText", typeof(string));
            excelDataTable.Columns.Add("TotalClaimAmount", typeof(decimal));
            excelDataTable.Columns.Add("MainDebt", typeof(decimal));
            excelDataTable.Columns.Add("CalculatedLateCharges", typeof(decimal));
            excelDataTable.Columns.Add("AmountOfFine", typeof(decimal));
            excelDataTable.Columns.Add("Percentage", typeof(decimal));
            excelDataTable.Columns.Add("AppealPredmet Id", typeof(int)); AppealTypeId
            excelDataTable.Columns.Add("AppealTypeId", typeof(int));
            excelDataTable.Columns.Add("Created By", typeof(string));
            excelDataTable.Columns.Add("Created Date", typeof(DateTime));
            excelDataTable.Columns.Add("Modify By", typeof(string));
            excelDataTable.Columns.Add("Modify Date", typeof(DateTime));


            var ApplicationsList = _mapper.Map<List<ApplicationResponse>>(AllApplications);

            if (ApplicationsList.Count > 0)
            {
                ApplicationsList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(item.Id, item.OrdinalNumber, item.Inn, item.FullName, item.Address,
                        item.PhoneNumber, item.ApplicationId, item.TypeOfApplicationId, item.CreatedBy,
                        item.CreatedDate, item.ModifyBy, item.ModifyDate);
                });
            }

            return excelDataTable;
        }
    }
}
/*


public decimal?  { get; set; }
public int AppealPredmetId { get; set; }
public int AppealTypeId { get; set; }
public DateTime IncomingDate { get; set; }

public DateTime CreatedDate { get; set; }
public DateTime ModifyDate { get; set; }
public string? CreatedBy { get; set; }
public string? ModifyBy { get; set; }