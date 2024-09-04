using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.User;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Application.Services.User
{
    public class UserPermissionService(
        IUserRepository userRepository,
        IUserPermissionRepository userPermissionRepository,
        IMapper mapper) : IUserPermissionService
    {
        public async Task<Response<PostUserPermissionResponse>> PostLinkUserPermission(PostUserPermissionRequest request)
        {
            var response = new Response<PostUserPermissionResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var userIdExists = await userRepository.UserIdExists(request.UserId);

            if (!userIdExists)
            {
                response.Errors.Add(GlobalMessages.USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var permissionExists = await userPermissionRepository.UserPermissionExistsById(request.ApplicationPermissionId);

            if (permissionExists)
            {
                response.Errors.Add(GlobalMessages.USER_PERMISSION_ALREADY);
                response.StatusCode = 409;
                return response;
            }

            var userPermission = mapper.Map<UserPermission>(request);

            await userPermissionRepository.LinkUserPermission(userPermission);

            var save = await userPermissionRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostUserPermissionResponse { Info = GlobalMessages.RECORD_SAVED_SUCCESSFULLY };

            userPermissionRepository.Dispose();

            return response;
        }

        public async Task<Response<DeleteUserPermissionResponse>> DeleteUserPermission(DeleteUserPermissionRequest request)
        {
            var response = new Response<DeleteUserPermissionResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var data = await userPermissionRepository.DeleteApplicationPermission(request.UserId, request.ApplicationPermissionId);

            if (!data)
            {
                response.Errors.Add(GlobalMessages.USER_OR_PERMISSION_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var save = await userPermissionRepository.SaveChangesAsync();

            userPermissionRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_DELETE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new DeleteUserPermissionResponse { Info = GlobalMessages.RECORD_DELETE_SUCCESSFULLY };

            return response;
        }

    }
}
