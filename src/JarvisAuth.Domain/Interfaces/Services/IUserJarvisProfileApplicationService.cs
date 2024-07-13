using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IUserJarvisProfileApplicationService
    {
        public Task<Response<PostLinkUserJarvisToApplicationResponse>> LinkUserJarvisToApplication(PostLinkUserJarvisToApplicationRequest request);

    }
}
