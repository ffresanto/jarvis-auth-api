using AutoMapper;
using JarvisAuth.Application.Security;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class JarvisService(IJarvisRepository jarvisRepository, IMapper mapper) : IJarvisService
    {
        public async Task<Response<PostCreateUserJarvisResponse>> PostCreateUserJarvis(PostCreateUserJarvisRequest request)
        {
            var response = new Response<PostCreateUserJarvisResponse>();
            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var userJarvis = mapper.Map<UserJarvis>(request);

            userJarvis.Password = EncryptionSecurity.EncryptPassword(userJarvis.Password);

            await jarvisRepository.CreateUserJarvis(userJarvis);

            var save = await jarvisRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add("A failure occurred while saving the user");
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostCreateUserJarvisResponse { UserId = userJarvis.Id };

            return response;
        }

        public async Task<Response<List<GetGenderTypeResponse>>> GetGendersTypes()
        {
            var response = new Response<List<GetGenderTypeResponse>>();

            var data = await jarvisRepository.GetGenderTypes();

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

            var data = await jarvisRepository.GetDocumentTypes();

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
