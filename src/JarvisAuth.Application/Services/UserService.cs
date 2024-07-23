using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.User;

namespace JarvisAuth.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
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
    }
}
