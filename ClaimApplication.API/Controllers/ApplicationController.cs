using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.DeleteApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.UpdateApplication;
using ClaimApplication.Application.UseCases.Applications.Queries.GetAllApplications;
using ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationById;
using ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationsPagination;
using ClaimApplication.Application.UseCases.Applications.Reports;
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

        [HttpPost("[action]")]
        public async Task<List<ApplicationResponse>> AddApplicationsFromExcel(IFormFile excelfile)
        {
            var result = await _mediator.Send(new AddApplicationsFromExcel(excelfile));
            return result;
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

        [HttpGet("[action]")]
        public async Task<FileResult> GetApplicationsInExcel(string fileName = "application")
        {
            var result = await _mediator.Send(new GetApplicationsExcel { FileName = fileName });
            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async Task<FileResult> GetApplicationByIdPDF(int id)
        {
            var result = await _mediator.Send(new GetApplicationByIdPDFQuery(id));
            return File(result.FileContents, result.Options, result.FileName);
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

