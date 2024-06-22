using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class TypeService(ITypesRepository typesRepository) : ITypeService
    {
        public async Task<Response<GetDocumentsTypesResponse>> GetDocumentsTypes()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetGendersTypesResponse>> GetGendersTypes()
        {
            throw new NotImplementedException();
        }
    }
}
