using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IUserJarvisService
    {
        public Task<Response<PostCreateUserJarvisResponse>> PostCreateUserJarvis(PostCreateUserJarvisRequest request);
        public Task<Response<PostUserJarvisLoginResponse>> PostLogin(PostLoginRequest request);
        public Task<Response<PostUserJarvisRefreshTokenResponse>> PostRefreshToken(PostRefreshTokenRequest request);
        public Task<Response<List<GetUserJarvisResponse>>> GetAllUserJarvis();
    }
}
