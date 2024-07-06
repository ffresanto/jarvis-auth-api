﻿using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Application.Services;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/application")]
    [Produces("application/json")]
    public class ApplicationController(IApplicationService applicationService) : BaseController
    {

        [HttpPost("create")]
        [SwaggerOperation(Summary = "The route creates a application for the Jarvis Auth.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostCreateApplicationResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateUserJarvis(PostCreateApplicationRequest request)
        {
            return CustomResponse(await applicationService.CreateApplication(request));
        }
    }
}
