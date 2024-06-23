using JarvisAuth.Core.Requests.System;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.System;
using JarvisAuth.Core.Responses.Types;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface ISystemService
    {
        public Task<Response<List<GetGenderTypeResponse>>> GetGendersTypes();
        public Task<Response<List<GetDocumentTypeResponse>>> GetDocumentsTypes();
        public Task<Response<PostCreateUserSystemResponse>> PostCreateUserSystem(PostCreateUserSystemRequest request);
    }
}
