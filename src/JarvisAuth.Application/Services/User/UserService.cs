using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.User;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Application.Services.User
{
    public class UserService(
        IUserRepository userRepository,
        IUserAssociateApplicationRepository userAssociateApplicationRepository,
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

            var user = mapper.Map<Domain.Entities.User>(request);

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

        public async Task<Response<PatchUserResponse>> PatchUser(PatchUserRequest request)
        {
            var response = new Response<PatchUserResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var user = await userRepository.FindUserById(request.UserId);

            if (user == null)
            {
                response.Errors.Add(GlobalMessages.USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await userRepository.UpdateUser(user);

            var save = await userRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PatchUserResponse { Info = GlobalMessages.RECORD_UPDATED_SUCCESSFULLY };

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

        public async Task<Response<PostAssociateUserToApplicationResponse>> PostAssociateApplication(PostAssociateUserToApplicationRequest request)
        {
            var response = new Response<PostAssociateUserToApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var isUserAssociatedToApplication = await userAssociateApplicationRepository.IsUserAssociateToApplication(request.ApplicationId, request.UserId);

            if (isUserAssociatedToApplication)
            {
                response.Errors.Add(GlobalMessages.USER_IS_ASSOCIATED_TO_APPLICATION);
                response.StatusCode = 409;
                return response;
            }

            var userIdJarvisExits = await userRepository.UserIdExists(request.UserId);

            if (!userIdJarvisExits)
            {
                response.Errors.Add(GlobalMessages.USER_NOT_EXISTS);
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

            var userProfileApplication = mapper.Map<UserAssociateApplication>(request);

            await userAssociateApplicationRepository.AssociateUserToApplication(userProfileApplication);

            var save = await userAssociateApplicationRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostAssociateUserToApplicationResponse { Info = GlobalMessages.RECORD_SAVED_SUCCESSFULLY };

            userRepository.Dispose();

            return response;
        }

        public async Task<Response<PatchApplicationToggleEnabledResponse>> PatchToggleEnabled(PatchUserToggleEnabledRequest request)
        {
            var response = new Response<PatchApplicationToggleEnabledResponse>();

            if (string.IsNullOrEmpty(request.UserId.ToString()))
            {
                response.Errors.Add(GlobalMessages.USER_ID_REQUIRED);
                response.StatusCode = 422;
                return response;
            }

            var user = await userRepository.FindUserById(request.UserId);

            if (user == null)
            {
                response.Errors.Add(GlobalMessages.USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            user.Enabled = request.Enable;

            await userRepository.UpdateUser(user);

            var save = await userRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PatchApplicationToggleEnabledResponse { Info = GlobalMessages.RECORD_UPDATED_SUCCESSFULLY };

            return response;
        }
    }
}
