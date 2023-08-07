using System.Web.Http;
using ClaimApplication.Application.UseCases.Applications.Reports;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ClaimApplication.API.Controllers;


[Route("api/[controller]")]
public class PdfController : BaseApiController
{
    [HttpPost("[action]")]
    public IActionResult GetHtmlTemplate()
    {
        var res = GetHtmlTemplate();

        return Ok(res);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<FormFile>> GetPdfTemplate(int id)
    {
        var result = await _mediator.Send(new GetApplicationPDFQuery(id));

        return File(result.FileContents, result.Options, result.FileName);
    }
}



