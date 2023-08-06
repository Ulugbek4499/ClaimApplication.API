using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.DeleteApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.UpdateApplication;
using ClaimApplication.Application.UseCases.Applications.Queries.GetAllApplications;
using ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationById;
using ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationsPagination;
using ClaimApplication.Application.UseCases.Applications.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateApplication(CreateApplicationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<ApplicationResponse> GetApplicationById(int Id)
        {
            return await _mediator.Send(new GetApplicationByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<ApplicationResponse>> GetAllApplications()
        {
            return await _mediator.Send(new GetAllApplicationsQuery());
        }

        [HttpGet("[action]")]
        public async ValueTask<ActionResult<PaginatedList<ApplicationResponse>>> GetAllApplicationsPagination(
            [FromQuery] GetApplicationsPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateApplication(UpdateApplicationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteApplication(DeleteApplicationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
