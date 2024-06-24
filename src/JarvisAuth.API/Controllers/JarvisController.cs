﻿using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/jarvis")]
    [Produces("application/json")]
    public class JarvisController(IJarvisService jarvisService) : BaseController
    {
        [HttpPost("users/create")]
        [SwaggerOperation(Summary = "The route creates a user for the Jarvis Auth.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<string>))]
        [SwaggerResponse(409, GlobalMessages.OPERATION_REQUEST_CONFLICT, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.OPERATION_VALIDATIONS_ERROS, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateUserJarvis(PostCreateUserJarvisRequest request)
        {
            return CustomResponse(await jarvisService.PostCreateUserJarvis(request));
        }
    }
}