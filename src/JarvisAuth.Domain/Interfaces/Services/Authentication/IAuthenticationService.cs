using JarvisAuth.Core.Requests.Authentication;
using JarvisAuth.Core.Responses.Authentication;
using JarvisAuth.Core.Responses.Shared;

namespace JarvisAuth.Domain.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<Response<PostLoginResponse>> PostLogin(PostLoginRequest request);
        public Task<Response<PostRefreshTokenResponse>> PostRefreshToken(PostRefreshTokenRequest request);
    }
}
