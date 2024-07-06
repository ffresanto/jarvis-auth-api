using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class ApplicationService(IApplicationRepository applicationRepository, IMapper mapper) : IApplicationService
    {
        public async Task<Response<PostCreateApplicationResponse>> CreateApplication(PostCreateApplicationRequest request)
        {
            var response = new Response<PostCreateApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var nameExists = await applicationRepository.ApplicationNameExists(request.Name);

            if (nameExists)
            {
                response.Errors.Add(GlobalMessages.NAME_ALREADY_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var application = mapper.Map<Domain.Entities.Application>(request);

            await applicationRepository.CreateApplication(application);

            var save = await applicationRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostCreateApplicationResponse { ApplicationId = application.Id };

            return response;
        }
    }
}
