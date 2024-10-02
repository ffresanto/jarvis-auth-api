using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class UserController(
        IUserService userService,
        IUserPermissionService userPermissionService
        ) : BaseController
    {
        [HttpGet()]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves a list of all users.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<List<GetUserResponse>>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplications()
        {
            return CustomResponse(await userService.GetAllUser());
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a new user for authentication system.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostUserResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostUser(PostUserRequest request)
        {
            return CustomResponse(await userService.PostUser(request));
        }

        [HttpPatch()]
        [SwaggerOperation(Summary = "Updates an existing User.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PatchUserResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PatchUser(PatchUserRequest request)
        {
            return CustomResponse(await userService.PatchUser(request));
        }

        [HttpPost("associate/application")]
        [Authorize]
        [SwaggerOperation(Summary = "Associate a user to an application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostAssociateUserToApplicationResponse>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostAssociateUserToApplication([FromBody] PostAssociateUserToApplicationRequest request)
        {
            return CustomResponse(await userService.PostAssociateApplication(request));
        }

        [HttpPost("associate/permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Associate a permission to an user.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostUserPermissionResponse>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostAssociateUserPermission([FromBody] PostUserPermissionRequest request)
        {
            return CustomResponse(await userPermissionService.PostAssociateUserPermission(request));
        }

        [HttpDelete("associate/permission")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete permission associated in user")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<DeleteUserPermissionResponse>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> DeleteUserPermission([FromBody] DeleteUserPermissionRequest request)
        {
            return CustomResponse(await userPermissionService.DeleteUserPermission(request));
        }

        [HttpPatch("toggle-enabled")]
        [Authorize]
        [SwaggerOperation(Summary = "Enables or disables a user.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PatchApplicationToggleEnabledResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PatchToggleEnabled([FromBody] PatchUserToggleEnabledRequest request)
        {
            return CustomResponse(await userService.PatchToggleEnabled(request));
        }
    }
}
