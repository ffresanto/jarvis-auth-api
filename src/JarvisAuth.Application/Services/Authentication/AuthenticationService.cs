using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Authentication;
using JarvisAuth.Core.Responses.Authentication;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.Authentication;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JarvisAuth.Application.Services.Authentication
{
    public class AuthenticationService(
        IUserRepository userRepository,
        IApplicationRepository applicationRepository,
        IConfiguration configuration) : IAuthenticationService
    {
        public async Task<Response<PostLoginResponse>> PostLogin(PostLoginRequest request)
        {
            var response = new Response<PostLoginResponse>();
            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var user = await userRepository.FindUserByEmail(request.Email);

            if (user == null)
            {
                response.Errors.Add(GlobalMessages.DATABASE_RECORD_NOT_FOUND);
                response.StatusCode = 404;
                return response;
            }

            if (user.Enabled == false)
            {
                response.Errors.Add(GlobalMessages.ACCOUNT_DISABLED);
                response.StatusCode = 403;
                return response;
            }

            if (!EncryptionSecurity.VerifyPasswordEncryption(request.Password, user.Password))
            {
                response.Errors.Add(GlobalMessages.INCORRECT_PASSWORD);
                response.StatusCode = 401;
                return response;
            }

            var permissions = await applicationRepository.FindApplicationWithPermissions(null, request.ApplicationName);

            if (permissions == null)
            {
                response.Errors.Add(GlobalMessages.APPLICATION_NOT_EXISTS);
                response.StatusCode = 404;
                return response;
            }

            var jwtToken = new JwtTokenSecurity(configuration);

            var accessTokenGenerate = jwtToken.GenerateJwtToken(user, permissions);
            var refreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user, permissions);

            response.Data = new PostLoginResponse { Token = accessTokenGenerate, RefreshToken = refreshTokenGenerate };

            userRepository.Dispose();

            return response;
        }
        public async Task<Response<PostRefreshTokenResponse>> PostRefreshToken(PostRefreshTokenRequest request)
        {
            var response = new Response<PostRefreshTokenResponse>();

            var jwtToken = new JwtTokenSecurity(configuration);

            var validateAccessToken = jwtToken.ValidateJwtToken(request.Token, false);
            var validateRefreshToken = jwtToken.ValidateJwtToken(request.RefreshToken, false);

            var accessTokenUserId = validateAccessToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var refreshTokenUserId = validateRefreshToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (accessTokenUserId != refreshTokenUserId)
            {
                response.Message = GlobalMessages.INVALID_TOKEN_OR_REFRESH_TOKEN;
                response.StatusCode = 500;
                return response;
            }

            var emailUserAccessToken = validateAccessToken.Claims.FirstOrDefault(c => c.Type == "userEmail")?.Value;

            var user = await userRepository.FindUserByEmail(emailUserAccessToken);

            if (user.Enabled == false)
            {
                response.Message = GlobalMessages.ACCOUNT_DISABLED;
                response.StatusCode = 403;
                return response;
            }

            var permissions = await applicationRepository.FindApplicationWithPermissions(null, request.ApplicationName);

            if (permissions == null)
            {
                response.Errors.Add(GlobalMessages.APPLICATION_NOT_EXISTS);
                response.StatusCode = 404;
                return response;
            }

            var newAccessTokenGenerate = jwtToken.GenerateJwtToken(user, permissions);
            var newRefreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user, permissions);

            response.Data = new PostRefreshTokenResponse { Token = newAccessTokenGenerate, RefreshToken = newRefreshTokenGenerate };

            userRepository.Dispose();

            return response;
        }

    }
}
