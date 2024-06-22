using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface ITypeService
    {
        public Task<Response<GetGendersTypesResponse>> GetGendersTypes();
        public Task<Response<GetDocumentsTypesResponse>> GetDocumentsTypes();
    }
}
