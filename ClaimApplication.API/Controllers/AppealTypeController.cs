﻿using ClaimApplication.Application.UseCases.AppealTypes.Commands.CreateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.DeleteAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.UpdateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAllAppealTypes;
using ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAppealTypeById;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppealTypeController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateAppealType(CreateAppealTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<AppealTypeResponse> GetAppealTypeById(int Id)
        {
            return await _mediator.Send(new GetAppealTypeByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<AppealTypeResponse>> GetAllAppealTypes()
        {
            return await _mediator.Send(new GetAllAppealTypesQuery());
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
