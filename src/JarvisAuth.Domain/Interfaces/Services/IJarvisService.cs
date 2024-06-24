using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IJarvisService
    {
        public Task<Response<PostCreateUserJarvisResponse>> PostCreateUserJarvis(PostCreateUserJarvisRequest request);
        public Task<Response<PostLoginResponse>> PostLogin(PostLoginRequest request);
    }
}
