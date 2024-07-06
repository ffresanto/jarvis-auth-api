using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IApplicationService
    {
        public Task<Response<PostCreateApplicationResponse>> CreateApplication(PostCreateApplicationRequest request);
    }
}
