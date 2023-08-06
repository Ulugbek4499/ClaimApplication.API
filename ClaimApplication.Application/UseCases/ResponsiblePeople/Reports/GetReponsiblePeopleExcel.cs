using System.Data;
using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Reports
{
    public class GetResponsiblePeopleExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetResponsiblePeopleExcelHandler : IRequestHandler<GetResponsiblePeopleExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetResponsiblePeopleExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetResponsiblePeopleExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetResponsiblePeopleAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "ResponsiblePeople");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 35;
                excelSheet.Column(2).Width = 15;
                excelSheet.Column(3).Width = 15;
                excelSheet.Column(4).Width = 35;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetResponsiblePeopleAsync(CancellationToken cancellationToken = default)
        {
            var AllResponsiblePeople = await _context.ResponsiblePeople.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Id", typeof(int));
            excelDataTable.Columns.Add("Name", typeof(string));
            excelDataTable.Columns.Add("Description", typeof(string));
            excelDataTable.Columns.Add("ResponsiblePersonTypeId", typeof(int));

            var ResponsiblePeopleList = _mapper.Map<List<ResponsiblePersonResponse>>(AllResponsiblePeople);

            if (ResponsiblePeopleList.Count > 0)
            {
                ResponsiblePeopleList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(item.Id, item.Inn, item.FullName, item.ApplicationId);
                });
            }

            return excelDataTable;
        }
    }
}
