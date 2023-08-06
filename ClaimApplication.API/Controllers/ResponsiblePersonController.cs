using ClaimApplication.API.Controllers;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.DeleteResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.UpdateResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Queries.GetResponsiblePeoplePagination;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Reports;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClaimResponsiblePeople.ResponsiblePeople.UseCases.ResponsiblePeople.Queries.GetAllResponsiblePeople;
using ClaimResponsiblePerson.ResponsiblePerson.UseCases.ResponsiblePeople.Queries.GetResponsiblePersonById;
using Microsoft.AspNetCore.Mvc;

namespace ClaimResponsiblePerson.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsiblePersonController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateResponsiblePerson(CreateResponsiblePersonCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<List<ResponsiblePersonResponse>> AddResponsiblePeopleFromExcel(IFormFile excelfile)
        {
            var result = await _mediator.Send(new AddResponsiblePeopleFromExcel(excelfile));
            return result;
        }

        [HttpGet("[action]")]
        public async ValueTask<ResponsiblePersonResponse> GetResponsiblePersonById(int Id)
        {
            return await _mediator.Send(new GetResponsiblePersonByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<ResponsiblePersonResponse>> GetAllResponsiblePersons()
        {
            return await _mediator.Send(new GetAllResponsiblePeopleQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<ResponsiblePersonResponse>>> GetAllResponsiblePersonsPagination(
            [FromQuery] GetResponsiblePeoplePaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<FileResult> GetResponsiblePeopleInExcel(string fileName = "responsiblepeople")
        {
            var result = await _mediator.Send(new GetResponsiblePeopleExcel { FileName = fileName });
            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async Task<FileResult> GetResponsiblePersonByIdPDF(int id)
        {
            var result = await _mediator.Send(new GetResponsiblePersonByIdPDFQuery(id));
            return File(result.FileContents, result.Options, result.FileName);
        }

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateResponsiblePerson(UpdateResponsiblePersonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteResponsiblePerson(DeleteResponsiblePersonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
