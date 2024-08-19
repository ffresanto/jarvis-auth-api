using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Authentication;
using JarvisAuth.Core.Responses.Authentication;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public class AuthenticationController(IAuthenticationService authenticationService) : BaseController
    {
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticates a user and provides a login token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostLoginResponse>))]
        [SwaggerResponse(401, GlobalMessages.UNAUTHORIZED_ACCESS_401, typeof(Response<string>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(404, GlobalMessages.REQUEST_NOT_FOUND_404, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.VALIDATION_ERRORS_422, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostLogin([FromBody] PostLoginRequest request)
        {
            return CustomResponse(await authenticationService.PostLogin(request));
        }

        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Generates a new refresh token.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCCESS_200, typeof(Response<PostRefreshTokenResponse>))]
        [SwaggerResponse(403, GlobalMessages.ACCESS_DENIED_403, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string>))]
        public async Task<ActionResult> PostRefreshToken([FromBody] PostRefreshTokenRequest request)
        {
            return CustomResponse(await authenticationService.PostRefreshToken(request));
        }
    }
}
