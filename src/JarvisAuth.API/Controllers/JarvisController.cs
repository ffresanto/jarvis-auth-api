using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
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
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostCreateUserJarvisResponse>))]
        [SwaggerResponse(409, GlobalMessages.REQUEST_CONFLICT_409, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateUserJarvis(PostCreateUserJarvisRequest request)
        {
            return CustomResponse(await jarvisService.PostCreateUserJarvis(request));
        }

        [HttpPost("auth/login")]
        [SwaggerOperation(Summary = "Authenticate using the user login")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostLoginResponse>))]
        [SwaggerResponse(401, GlobalMessages.UNAUTHORIZED_ACCESS_401, typeof(Response<string>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostLogin([FromBody] PostLoginRequest request)
        {
            return CustomResponse(await jarvisService.PostLogin(request));
        }

        [HttpPost("auth/refresh-token")]
        [SwaggerOperation(Summary = "Generate Refresh Token")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostRefreshTokenResponse>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostRefreshToken([FromBody] PostRefreshTokenRequest request)
        {
            return CustomResponse(await jarvisService.PostRefreshToken(request));
        }

    }
}
