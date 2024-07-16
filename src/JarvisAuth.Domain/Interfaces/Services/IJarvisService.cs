using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IJarvisService
    {
        public Task<Response<PostUserJarvisResponse>> PostUserJarvis(PostUserJarvisRequest request);
        public Task<Response<PostJarvisLoginResponse>> PostLogin(PostJarvisLoginRequest request);
        public Task<Response<PostJarvisRefreshTokenResponse>> PostRefreshToken(PostJarvisRefreshTokenRequest request);
        public Task<Response<List<GetUserJarvisResponse>>> GetAllUserJarvis();
        public Task<Response<PostLinkUserJarvisToApplicationResponse>> PostLinkApplication(PostLinkUserJarvisToApplicationRequest request);
    }
}
