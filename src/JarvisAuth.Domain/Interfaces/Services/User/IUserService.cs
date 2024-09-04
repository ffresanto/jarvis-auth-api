using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;

namespace JarvisAuth.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        public Task<Response<PostUserResponse>> PostUser(PostUserRequest request);
        public Task<Response<PatchUserResponse>> PatchUser(PatchUserRequest request);
        public Task<Response<List<GetUserResponse>>> GetAllUser();
        public Task<Response<PostLinkUserToApplicationResponse>> PostLinkApplication(PostLinkUserToApplicationRequest request);
        public Task<Response<PatchApplicationToggleEnabledResponse>> PatchToggleEnabled(PatchUserToggleEnabledRequest request);
    }
}
