using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;

namespace JarvisAuth.Domain.Interfaces.Services.Jarvis
{
    public interface IUserJarvisLinkedApplicationService
    {
        public Task<Response<PostLinkUserJarvisToApplicationResponse>> LinkUserJarvisToApplication(PostLinkUserJarvisToApplicationRequest request);

    }
}
