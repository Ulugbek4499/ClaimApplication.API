using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Reports
{
    public record AddResponsiblePeopleFromExcel(IFormFile ExcelFile) : IRequest<List<ResponsiblePersonResponse>>;

    public class AddResponsiblePeopleFromExcelHandler : IRequestHandler<AddResponsiblePeopleFromExcel, List<ResponsiblePersonResponse>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddResponsiblePeopleFromExcelHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResponsiblePersonResponse>> Handle(AddResponsiblePeopleFromExcel request, CancellationToken cancellationToken)
        {
            if (request.ExcelFile == null || request.ExcelFile.Length == 0)
                throw new ArgumentNullException("File", "file is empty or null");

            var file = request.ExcelFile;
            List<ResponsiblePerson> result = new();
            using (var ms = new MemoryStream())
            {

                await file.CopyToAsync(ms, cancellationToken);
                using (var wb = new XLWorkbook(ms))
                {
                    var sheet1 = wb.Worksheet(1);
                    int startRow = 2;
                    for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
                    {
                        var ResponsiblePerson = new ResponsiblePerson()
                        {
                            OrdinalNumber = sheet1.Cell(row, 1).GetString(),
                            Inn = sheet1.Cell(row, 2).GetString(),
                            FullName = sheet1.Cell(row, 3).GetString(),
                            Address= sheet1.Cell(row, 4).GetString(),
                            PhoneNumber= sheet1.Cell(row, 5).GetString(),
                            ApplicationId = int.Parse(sheet1.Cell(row, 6).GetString()),
                            TypeOfResponsiblePersonId = int.Parse(sheet1.Cell(row, 6).GetString()),
                        };

                        result.Add(ResponsiblePerson);
                    }
                }
            }
            await _context.ResponsiblePeople.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<ResponsiblePersonResponse>>(result);
        }
    }
}
