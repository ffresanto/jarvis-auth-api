using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;
using JarvisAuth.Domain.Interfaces.Services.Jarvis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/jarvis")]
    [Produces("application/json")]
    public class JarvisController(IJarvisService jarvisService) : BaseController
    {
        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a new user for the Jarvis authentication system.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostUserJarvisResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostUserJarvis(PostUserJarvisRequest request)
        {
            return CustomResponse(await jarvisService.PostUserJarvis(request));
        }

        [HttpGet()]
        [Authorize]
        [SwaggerOperation(Summary = "Retrieves a list of all user jarvis.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<List<GetUserJarvisResponse>>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> GetApplications()
        {
            return CustomResponse(await jarvisService.GetAllUserJarvis());
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticates a user and provides a login token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostJarvisLoginResponse>))]
        [SwaggerResponse(401, GlobalMessages.UNAUTHORIZED_ACCESS_401, typeof(Response<string>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostLogin([FromBody] PostJarvisLoginRequest request)
        {
            return CustomResponse(await jarvisService.PostLogin(request));
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Generates a new refresh token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostJarvisRefreshTokenResponse>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostRefreshToken([FromBody] PostJarvisRefreshTokenRequest request)
        {
            return CustomResponse(await jarvisService.PostRefreshToken(request));
        }

        [HttpPost("link-application")]
        [Authorize]
        [SwaggerOperation(Summary = "Link a user Jarvis to an application.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostLinkUserJarvisToApplicationResponse>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> LinkUserJarvisToApplication([FromBody] PostLinkUserJarvisToApplicationRequest request)
        {
            return CustomResponse(await jarvisService.PostLinkApplication(request));
        }
    }
}
