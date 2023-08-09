using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.Memberships.Commands.DeleteMembership;
using ClaimApplication.Application.UseCases.Memberships.Queries.GetAllMemberships;
using ClaimApplication.Application.UseCases.Memberships.Queries.GetMembershipById;
using ClaimMembershipApplication.MembershipApplication.UseCases.MembershipApplications.Commands.CreateMembershipApplication;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipApplicationController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateMembershipApplication(CreateMembershipApplicationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<MembershipApplicationResponse> GetMembershipApplicationById(int Id)
        {
            return await _mediator.Send(new GetMembershipApplicationByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<MembershipApplicationResponse>> GetAllMembershipApplications()
        {
            return (IEnumerable<MembershipApplicationResponse>)await _mediator.Send(
                new GetMembershipApplicationsQuery());
        }

    
        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteMembershipApplication(DeleteMembershipApplicationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
