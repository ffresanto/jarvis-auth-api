using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class SystemService(ISystemRepository systemRepository, IMapper mapper) : ISystemService
    {
        public async Task<Response<List<GetGenderTypeResponse>>> GetGendersTypes()
        {
            var response = new Response<List<GetGenderTypeResponse>>();

            var data = await systemRepository.GetGenderTypes();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE);
                response.StatusCode = 404;
                return response;
            }

            response.Data = mapper.Map<List<GetGenderTypeResponse>>(data);

            return response;
        }

        public async Task<Response<List<GetDocumentTypeResponse>>> GetDocumentsTypes()
        {
            var response = new Response<List<GetDocumentTypeResponse>>();

            var data = await systemRepository.GetDocumentTypes();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.RECORDS_NOT_FOUND_IN_DATABASE);
                response.StatusCode = 404;
                return response;
            }

            response.Data = mapper.Map<List<GetDocumentTypeResponse>>(data);

            return response;
        }
    }
}
