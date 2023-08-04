using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.CreateTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.DeleteTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.UpdateTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetAllTypeOfResponsiblePeople;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetTypeOfResponsiblePeoplePagination;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetTypeOfResponsiblePersonById;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfResponsiblePersonController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateTypeOfResponsiblePerson(CreateTypeOfResponsiblePersonCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<TypeOfResponsiblePersonResponse> GetTypeOfResponsiblePersonById(Guid Id)
        {
            return await _mediator.Send(new GetTypeOfResponsiblePersonByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<TypeOfResponsiblePersonResponse>> GetAllTypeOfResponsiblePeople()
        {
            return await _mediator.Send(new GetAllTypeOfResponsiblePersonsQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<TypeOfResponsiblePersonResponse>>> GetAllTypeOfResponsiblePersonsPagination(
            [FromQuery] GetTypeOfResponsiblePersonsPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateTypeOfResponsiblePerson(UpdateTypeOfResponsiblePersonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteTypeOfResponsiblePerson(DeleteTypeOfResponsiblePersonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
