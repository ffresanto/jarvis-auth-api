using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IApplicationService
    {
        public Task<Response<PostApplicationResponse>> PostApplication(PostApplicationRequest request);
        public Task<Response<List<GetApplicationResponse>>> GetApplications();
    }
}
