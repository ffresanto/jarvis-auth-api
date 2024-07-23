using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.User;
using JarvisAuth.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace JarvisAuth.Application.Services
{
    public class UserService(
        IConfiguration configuration,
        IUserRepository userRepository,
        IUserLinkedApplicationRepository userLinkedApplicationRepository,
        IApplicationRepository applicationRepository,
        IMapper mapper) : IUserService
    {
        public async Task<Response<PostUserResponse>> PostUser(PostUserRequest request)
        {
            var response = new Response<PostUserResponse>();
            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var emailExists = await userRepository.UserEmailExists(request.Email);

            if (emailExists)
            {
                response.Errors.Add(GlobalMessages.EMAIL_ALREADY_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var user = mapper.Map<User>(request);

            user.Password = EncryptionSecurity.EncryptPassword(user.Password);

            await userRepository.CreateUser(user);

            var save = await userRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostUserResponse { UserId = user.Id };

            userRepository.Dispose();

            return response;
        }

        public async Task<Response<PostUserLoginResponse>> PostLogin(PostUserLoginRequest request)
        {
            var response = new Response<PostUserLoginResponse>();
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

            var jwtToken = new JwtTokenSecurity(configuration);

            var accessTokenGenerate = jwtToken.GenerateJwtToken(user);
            var refreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user);

            response.Data = new PostUserLoginResponse { Token = accessTokenGenerate, RefreshToken = refreshTokenGenerate };

            userRepository.Dispose();

            return response;
        }
        public async Task<Response<PostUserRefreshTokenResponse>> PostRefreshToken(PostUserRefreshTokenRequest request)
        {
            var response = new Response<PostUserRefreshTokenResponse>();

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

            var newAccessTokenGenerate = jwtToken.GenerateJwtToken(user);
            var newRefreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user);

            response.Data = new PostUserRefreshTokenResponse { Token = newAccessTokenGenerate, RefreshToken = newRefreshTokenGenerate };

            userRepository.Dispose();

            return response;
        }

        public async Task<Response<List<GetUserResponse>>> GetAllUser()
        {
            var response = new Response<List<GetUserResponse>>();

            var data = await userRepository.GetAllUser();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.DATABASE_RECORD_NOT_FOUND);
                response.StatusCode = 404;
                return response;
            }

            var dataMapper = mapper.Map<List<GetUserResponse>>(data);

            response.Data = dataMapper;

            userRepository.Dispose();

            return response;
        }

        public async Task<Response<PostLinkUserToApplicationResponse>> PostLinkApplication(PostLinkUserToApplicationRequest request)
        {
            var response = new Response<PostLinkUserToApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var isUserLinkedToApplication = await userLinkedApplicationRepository.IsUserLinkedToApplication(request.ApplicationId, request.UserJarvisId);

            if (isUserLinkedToApplication)
            {
                response.Errors.Add(GlobalMessages.USER_IS_LINKED_TO_APPLICATION);
                response.StatusCode = 409;
                return response;
            }

            var userIdJarvisExits = await userRepository.UserIdExists(request.UserJarvisId);

            if (!userIdJarvisExits)
            {
                response.Errors.Add(GlobalMessages.JARVIS_USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var applicationIdExists = await applicationRepository.ApplicationIdExists(request.ApplicationId);

            if (!applicationIdExists)
            {
                response.Errors.Add(GlobalMessages.APPLICATION_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var userJarvisProfileApplication = mapper.Map<UserLinkedApplication>(request);

            await userLinkedApplicationRepository.LinkUserToApplication(userJarvisProfileApplication);

            var save = await userLinkedApplicationRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostLinkUserToApplicationResponse { Info = GlobalMessages.RECORD_SAVED_SUCCESSFULLY };

            userRepository.Dispose();

            return response;
        }
    }
}
