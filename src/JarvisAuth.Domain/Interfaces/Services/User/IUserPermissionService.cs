using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;

namespace JarvisAuth.Domain.Interfaces.Services.User
{
    public interface IUserPermissionService
    {
        public Task<Response<PostUserPermissionResponse>> PostLinkUserPermission(PostUserPermissionRequest request);
    }
}
