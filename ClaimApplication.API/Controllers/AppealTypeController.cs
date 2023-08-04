using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.CreateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.DeleteAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.UpdateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAllAppealTypes;
using ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAppealTypeById;
using ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAppealTypesPagination;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppealTypeController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateAppealType(CreateAppealTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<AppealTypeResponse> GetAppealTypeById(Guid Id)
        {
            return await _mediator.Send(new GetAppealTypeByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<AppealTypeResponse>> GetAllAppealTypes()
        {
            return await _mediator.Send(new GetAllAppealTypesQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<AppealTypeResponse>>> GetAllAppealTypesPagination(
            [FromQuery] GetAppealTypesPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateAppealType(UpdateAppealTypeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteAppealType(DeleteAppealTypeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
