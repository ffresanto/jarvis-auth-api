using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;

namespace JarvisAuth.Application.Services
{
    public class ApplicationService(
        IApplicationRepository applicationRepository,
        IMapper mapper) : IApplicationService
    {
        public async Task<Response<PostApplicationResponse>> PostApplication(PostApplicationRequest request)
        {
            var response = new Response<PostApplicationResponse>();

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

            response.Data = new PostApplicationResponse { ApplicationId = application.Id };

            return response;
        }

        public async Task<Response<List<GetApplicationResponse>>> GetApplications()
        {
            var response = new Response<List<GetApplicationResponse>>();

            var data = await applicationRepository.GetAllApplications();

            if (data == null)
            {
                response.Errors.Add(GlobalMessages.DATABASE_RECORD_NOT_FOUND);
                response.StatusCode = 404;
                return response;
            }

            var dataMapper = mapper.Map<List<GetApplicationResponse>>(data);

            response.Data = dataMapper;

            return response;
        }
    }
}
