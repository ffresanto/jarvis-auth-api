using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Interfaces.Services.Application;

namespace JarvisAuth.Application.Services.Application
{
    public class ApplicationService(
        IApplicationRepository applicationRepository,
        IApplicationPermissionRepository applicationPermissionRepository,
        IUserPermissionRepository userPermissionRepository,
        IMapper mapper) : IApplicationService
    {
        public async Task<Response<PostApplicationResponse>> PostApplication(PostApplicationRequest request)
        {
            var response = new Response<PostApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var nameExists = await applicationRepository.ApplicationNameExists(request.Name);

            if (nameExists)
            {
                response.Errors.Add(GlobalMessages.NAME_ALREADY_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var application = mapper.Map<Domain.Entities.Application>(request);

            await applicationRepository.CreateApplication(application);

            var save = await applicationRepository.SaveChangesAsync();

            applicationRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostApplicationResponse { ApplicationId = application.Id };

            return response;
        }

        public async Task<Response<List<GetApplicationResponse>>> GetApplications()
        {
            var response = new Response<List<GetApplicationResponse>>();

            var data = await applicationRepository.GetAllApplications();

            applicationRepository.Dispose();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.DATABASE_RECORD_NOT_FOUND);
                response.StatusCode = 404;
                return response;
            }

            var dataMapper = mapper.Map<List<GetApplicationResponse>>(data);

            response.Data = dataMapper;

            return response;
        }

        public async Task<Response<PostApplicationPermissionResponse>> PostApplicationPermission(PostApplicationPermissionRequest request)
        {
            var response = new Response<PostApplicationPermissionResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var nameExists = await applicationPermissionRepository.ApplicationPermissionNameExists(request.Name);

            if (nameExists)
            {
                response.Errors.Add(GlobalMessages.NAME_ALREADY_EXISTS);
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

            var applicationPermission = mapper.Map<ApplicationPermission>(request);

            await applicationPermissionRepository.CreateApplicationPermission(applicationPermission);

            var save = await applicationPermissionRepository.SaveChangesAsync();

            applicationPermissionRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostApplicationPermissionResponse { ApplicationPermissionId = applicationPermission.Id };

            return response;
        }

        public async Task<Response<List<GetApplicationWithPermissionsResponse>>> GetFindApplicationWithPermissions(Guid? applicationId, string permissionName)
        {
            var response = new Response<List<GetApplicationWithPermissionsResponse>>();

            if (string.IsNullOrEmpty(applicationId.ToString()) && string.IsNullOrEmpty(permissionName))
            {
                response.Errors.Add(GlobalMessages.PROVIDER_APPLICATION_AND_PERMISSION);
                response.StatusCode = 422;
                return response;
            }

            var data = await applicationRepository.FindApplicationWithListPermissions(applicationId, permissionName);

            applicationRepository.Dispose();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.DATABASE_RECORD_NOT_FOUND);
                response.StatusCode = 404;
                return response;
            }

            var applicationWithPermission = mapper.Map<List<GetApplicationWithPermissionsResponse>>(data);

            response.Data = applicationWithPermission;

            return response;
        }

        public async Task<Response<PatchApplicationToggleEnabledResponse>> PatchToggleEnabled(PatchApplicationToggleEnabledRequest request)
        {
            var response = new Response<PatchApplicationToggleEnabledResponse>();

            if (string.IsNullOrEmpty(request.ApplicationId.ToString()))
            {
                response.Errors.Add(GlobalMessages.APPLICATION_ID_REQUIRED);
                response.StatusCode = 422;
                return response;
            }

            var application = await applicationRepository.FindApplicationById(request.ApplicationId);

            if (application == null)
            {
                response.Errors.Add(GlobalMessages.USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            application.Enabled = request.Enable;
            application.UpdatedAt = DateTime.UtcNow;

            await applicationRepository.UpdateApplication(application);

            var save = await applicationRepository.SaveChangesAsync();

            applicationRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PatchApplicationToggleEnabledResponse { Info = GlobalMessages.RECORD_UPDATED_SUCCESSFULLY };

            return response;
        }

        public async Task<Response<DeleteApplicationPermissionResponse>> DeleteApplicationPermission(DeleteApplicationPermissionRequest request)
        {
            var response = new Response<DeleteApplicationPermissionResponse>();

            if (string.IsNullOrEmpty(request.PermissionId.ToString()))
            {
                response.Errors.Add(GlobalMessages.PERMISSION_ID_REQUIRED);
                response.StatusCode = 422;
                return response;
            }

            var permissionExists = await userPermissionRepository.UserPermissionExistsById(request.PermissionId);

            if (permissionExists)
            {
                response.Errors.Add(GlobalMessages.PERMISSION_LINKED_USER);
                response.StatusCode = 409;
                return response;
            }

            var data = await applicationPermissionRepository.DeleteApplicationPermission(request.PermissionId);

            if (!data)
            {
                response.Errors.Add(GlobalMessages.PERMISSION_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var save = await applicationPermissionRepository.SaveChangesAsync();

            applicationPermissionRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_DELETE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new DeleteApplicationPermissionResponse { Info = GlobalMessages.RECORD_DELETE_SUCCESSFULLY };

            return response;
        }

        public async Task<Response<PatchApplicationResponse>> PatchApplication(PatchApplicationRequest request)
        {
            var response = new Response<PatchApplicationResponse>();

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Errors.Add(GlobalMessages.MANDATORY_APPLICATION_NAME);
                response.StatusCode = 422;
                return response;
            }

            var application = await applicationRepository.FindApplicationById(request.ApplicationId);

            if (application == null)
            {
                response.Errors.Add(GlobalMessages.APPLICATION_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            application.Name = request.Name;
            application.UpdatedAt = DateTime.UtcNow;

            await applicationRepository.UpdateApplication(application);

            var save = await applicationRepository.SaveChangesAsync();

            applicationRepository.Dispose();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PatchApplicationResponse { Info = GlobalMessages.RECORD_UPDATED_SUCCESSFULLY };

            return response;
        }
    }
}
