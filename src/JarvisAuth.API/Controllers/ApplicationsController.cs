using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    [Produces("application/json")]
    public class ApplicationsController(IApplicationService applicationService) : BaseController
    {

        [HttpPost("create")]
        [Authorize]
        [SwaggerOperation(Summary = "Creates a new application for the Jarvis authentication system.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostCreateApplicationResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateApplication(PostCreateApplicationRequest request)
        {
            return CustomResponse(await applicationService.CreateApplication(request));
        }

        [HttpGet()]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves a list of all applications.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostCreateApplicationResponse>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplications()
        {
            return CustomResponse(await applicationService.GetApplications());
        }
    }
}
