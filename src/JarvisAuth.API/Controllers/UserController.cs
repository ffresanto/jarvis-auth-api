using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
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
    public class UserController(IUserService userService) : BaseController
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

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticates a user and provides a login token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostUserLoginResponse>))]
        [SwaggerResponse(401, GlobalMessages.UNAUTHORIZED_ACCESS_401, typeof(Response<string>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostLogin([FromBody] PostUserLoginRequest request)
        {
            return CustomResponse(await userService.PostLogin(request));
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Generates a new refresh token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostUserRefreshTokenResponse>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostRefreshToken([FromBody] PostUserRefreshTokenRequest request)
        {
            return CustomResponse(await userService.PostRefreshToken(request));
        }

        [HttpPost("link-application")]
        [Authorize]
        [SwaggerOperation(Summary = "Link a user to an application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostLinkUserToApplicationResponse>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> LinkUserToApplication([FromBody] PostLinkUserToApplicationRequest request)
        {
            return CustomResponse(await userService.PostLinkApplication(request));
        }
    }
}
