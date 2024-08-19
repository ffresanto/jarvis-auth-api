using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/application")]
    [Produces("application/json")]
    public class ApplicationController(IApplicationService applicationService) : BaseController
    {
        [HttpGet()]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves a list of all applications.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<List<GetApplicationResponse>>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplications()
        {
            return CustomResponse(await applicationService.GetApplications());
        }

        [HttpPost()]
        [Authorize]
        [SwaggerOperation(Summary = "Creates a new application for the Jarvis authentication system.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostApplicationResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateApplication(PostApplicationRequest request)
        {
            return CustomResponse(await applicationService.PostApplication(request));
        }

        [HttpPost("permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Creates permission for a specific application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostApplicationPermissionResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostApplicationPermission(PostApplicationPermissionRequest request)
        {
            return CustomResponse(await applicationService.PostApplicationPermission(request));
        }

        [HttpGet("permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves the list of permissions linked to the searched application")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<GetApplicationWithPermissionsResponse>))]
        [SwaggerResponse(404, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplicationWithPermissions([FromQuery] Guid? applicationId, [FromQuery] string permissionName = "")
        {
            return CustomResponse(await applicationService.GetFindApplicationWithPermissions(applicationId, permissionName));
        }
    }
}
