using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface IJarvisService
    {
        public Task<Response<List<GetGenderTypeResponse>>> GetGendersTypes();
        public Task<Response<List<GetDocumentTypeResponse>>> GetDocumentsTypes();
        public Task<Response<PostCreateUserJarvisResponse>> PostCreateUserJarvis(PostCreateUserJarvisRequest request);
    }
}
