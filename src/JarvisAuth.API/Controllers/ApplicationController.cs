using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Application.Security.Attributes;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<List<GetApplicationResponse>>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplications()
        {
            return CustomResponse(await applicationService.GetApplications());
        }

        [HttpGet("permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves the list of permissions associate to the searched application")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<List<GetApplicationWithPermissionsResponse>>))]
        [SwaggerResponse(404, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplicationWithPermissions([FromQuery] Guid? applicationId, [FromQuery] string applicationName = "")
        {
            return CustomResponse(await applicationService.GetFindApplicationWithPermissions(applicationId, applicationName));
        }

        [HttpPost()]
        [Authorize]
        [OnlyAdministrator]
        [SwaggerOperation(Summary = "Creates a new application for the Jarvis authentication system.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostApplicationResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateApplication(PostApplicationRequest request)
        {
            return CustomResponse(await applicationService.PostApplication(request));
        }

        [HttpPatch()]
        [OnlyAdministrator]
        [SwaggerOperation(Summary = "Updates an existing application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PatchApplicationResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PatchUser(PatchApplicationRequest request)
        {
            return CustomResponse(await applicationService.PatchApplication(request));
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

        [HttpDelete("permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete permission for id.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<DeleteApplicationPermissionResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> DeleteApplicationPermission(DeleteApplicationPermissionRequest request)
        {
            return CustomResponse(await applicationService.DeleteApplicationPermission(request));
        }

        [HttpPatch("toggle-enabled")]
        [Authorize]
        [SwaggerOperation(Summary = "Enables or disables a application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PatchApplicationToggleEnabledResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PatchToggleEnabled([FromBody] PatchApplicationToggleEnabledRequest request)
        {
            return CustomResponse(await applicationService.PatchToggleEnabled(request));
        }
    }
}
