using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;

namespace JarvisAuth.Domain.Interfaces.Services.Application
{
    public interface IApplicationService
    {
        public Task<Response<PostApplicationResponse>> PostApplication(PostApplicationRequest request);
        public Task<Response<List<GetApplicationResponse>>> GetApplications();  
        public Task<Response<PostApplicationPermissionResponse>> PostApplicationPermission(PostApplicationPermissionRequest request);
        public Task<Response<GetApplicationWithPermissionsResponse>> GetFindApplicationWithPermissions(Guid? applicationId, string permissionName);
        public Task<Response<PatchApplicationToggleEnabledResponse>> PatchToggleEnabled(PatchApplicationToggleEnabledRequest request);
    }
}
