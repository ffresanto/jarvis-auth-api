using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class JarvisService(IJarvisRepository jarvisRepository, IMapper mapper) : IJarvisService
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
    }
}
