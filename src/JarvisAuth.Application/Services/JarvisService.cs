using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Data;
using static JarvisAuth.Application.Security.JwtTokenSecurity;

namespace JarvisAuth.Application.Services
{
    public class JarvisService(IConfiguration configuration, IJarvisRepository jarvisRepository, IMapper mapper) : IJarvisService
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

            var emailExists = await jarvisRepository.EmailExistsAsync(request.Email);

            if (emailExists)
            {
                response.Errors.Add("Email already exists");
                response.StatusCode = 409;
                return response;
            }

            var userJarvis = mapper.Map<UserJarvis>(request);

            userJarvis.Password = EncryptionSecurity.EncryptPassword(userJarvis.Password);

            await jarvisRepository.CreateUserJarvis(userJarvis);

            var save = await jarvisRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add("A failure occurred while saving the user");
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostCreateUserJarvisResponse { UserId = userJarvis.Id };

            return response;
        }

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

            var user = await jarvisRepository.FindUserByEmail(request.Email);

            if (user == null)
            {
                response.Errors.Add(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE);
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
                response.Errors.Add(GlobalMessages.PASSWORD_INCORRECT);
                response.StatusCode = 401;
                return response;
            }

            var jwtToken = new JwtToken(configuration);

            var accessTokenGenerate = jwtToken.GenerateJwtToken(user);
            var refreshTokenGenerate = jwtToken.GenerateRefreshJwtToken(user);

            response.Data = new PostLoginResponse { Token = accessTokenGenerate, RefreshToken = refreshTokenGenerate };

            return response;
        }
    }
}
