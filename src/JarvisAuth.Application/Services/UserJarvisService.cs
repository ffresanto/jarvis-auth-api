using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;
using JarvisAuth.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace JarvisAuth.Application.Services
{
    public class UserJarvisService(
        IConfiguration configuration,
        IUserJarvisRepository userJarvisRepository,
        IUserJarvisProfileApplicationRepository userJarvisProfileApplicationRepository,
        IMapper mapper) : IUserJarvisService
    {
        public async Task<Response<PostCreateUserJarvisResponse>> PostCreateUserJarvis(PostCreateUserJarvisRequest request)
        {
            var response = new Response<PostCreateUserJarvisResponse>();
            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var emailExists = await userJarvisRepository.UserEmailExists(request.Email);

            if (emailExists)
            {
                response.Errors.Add(GlobalMessages.EMAIL_ALREADY_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var userJarvis = mapper.Map<UserJarvis>(request);

            userJarvis.Password = EncryptionSecurity.EncryptPassword(userJarvis.Password);

            await userJarvisRepository.CreateUserJarvis(userJarvis);

            var save = await userJarvisRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostCreateUserJarvisResponse { UserId = userJarvis.Id };

            return response;
        }

        public async Task<Response<PostUserJarvisLoginResponse>> PostLogin(PostLoginRequest request)
        {
            var response = new Response<PostUserJarvisLoginResponse>();
            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var user = await userJarvisRepository.FindUserByEmail(request.Email);

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

            var jwtToken = new JwtTokenSecurity(configuration);

            var accessTokenGenerate = jwtToken.GenerateJwtToken(user);
            var refreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user);

            response.Data = new PostUserJarvisLoginResponse { Token = accessTokenGenerate, RefreshToken = refreshTokenGenerate };

            return response;
        }
        public async Task<Response<PostUserJarvisRefreshTokenResponse>> PostRefreshToken(PostRefreshTokenRequest request)
        {
            var response = new Response<PostUserJarvisRefreshTokenResponse>();

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

            var user = await userJarvisRepository.FindUserByEmail(emailUserAccessToken);

            if (user.Enabled == false)
            {
                response.Message = GlobalMessages.ACCOUNT_DISABLED;
                response.StatusCode = 403;
                return response;
            }

            var newAccessTokenGenerate = jwtToken.GenerateJwtToken(user);
            var newRefreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user);

            response.Data = new PostUserJarvisRefreshTokenResponse { Token = newAccessTokenGenerate, RefreshToken = newRefreshTokenGenerate };

            return response;
        }
        public async Task<Response<PostLinkUserJarvisToApplicationResponse>> PostLinkUserJarvisToApplication(PostLinkUserJarvisToApplicationRequest request)
        {
            var response = new Response<PostLinkUserJarvisToApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var userJarvisProfileApplication = mapper.Map<UserJarvisProfileApplication>(request);

            await userJarvisProfileApplicationRepository.LinkUserJarvisToApplication(userJarvisProfileApplication);
            
            var save = await userJarvisProfileApplicationRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostLinkUserJarvisToApplicationResponse { Message= GlobalMessages.RECORD_SAVED_SUCCESSFULLY };

            return response;
        }

    }
}
