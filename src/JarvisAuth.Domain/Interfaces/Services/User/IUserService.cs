using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;

namespace JarvisAuth.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        public Task<Response<PostUserResponse>> PostUser(PostUserRequest request);
    }
}
