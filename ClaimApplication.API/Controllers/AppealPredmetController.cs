using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.CreateAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.DeleteAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.UpdateAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAllAppealPredmets;
using ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAppealPredmetById;
using ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAppealPredmetsPagination;
using ClaimApplication.Application.UseCases.AppealPredmets.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppealPredmetController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateAppealPredmet(CreateAppealPredmetCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<AppealPredmetResponse> GetAppealPredmetById(int Id)
        {
            return await _mediator.Send(new GetAppealPredmetByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<AppealPredmetResponse>> GetAllAppealPredmets()
        {
            return await _mediator.Send(new GetAllAppealPredmetsQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<AppealPredmetResponse>>> GetAllAppealPredmetsPagination(
            [FromQuery] GetAppealPredmetsPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateAppealPredmet(UpdateAppealPredmetCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteAppealPredmet(DeleteAppealPredmetCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
