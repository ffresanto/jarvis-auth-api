using AutoMapper;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.UserJarvis;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Services;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Application.Services
{
    public class ApplicationService(
        IApplicationRepository applicationRepository,
        IUserJarvisProfileApplicationRepository userJarvisProfileApplicationRepository,
        IUserJarvisRepository userJarvisRepository,
        IMapper mapper) : IApplicationService
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

        public async Task<Response<List<GetApplicationResponse>>> GetApplications()
        {
            var response = new Response<List<GetApplicationResponse>>();

            var data = await applicationRepository.GetApplications();

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

        public async Task<Response<PostLinkUserJarvisToApplicationResponse>> PostLinkUserJarvisToApplication(PostLinkUserJarvisToApplicationRequest request)
        {
            var response = new Response<PostLinkUserJarvisToApplicationResponse>();

            var validate = request.Validate(request);

            if (validate.Count > 0)
            {
                response.Errors = validate;
                response.StatusCode = 422;
                return response;
            }

            var isUserLinkedToApplication = await userJarvisProfileApplicationRepository.IsUserLinkedToApplication(request.ApplicationId, request.UserJarvisId);

            if (isUserLinkedToApplication)
            {
                response.Errors.Add(GlobalMessages.USER_IS_LINKED_TO_APPLICATION);
                response.StatusCode = 409;
                return response;
            }

            var userIdJarvisExits = await userJarvisRepository.UserIdExists(request.UserJarvisId);

            if (!userIdJarvisExits)
            {
                response.Errors.Add(GlobalMessages.JARVIS_USER_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var applicationIdExists = await applicationRepository.ApplicationIdExists(request.ApplicationId);

            if (!applicationIdExists)
            {
                response.Errors.Add(GlobalMessages.APPLICATION_NOT_EXISTS);
                response.StatusCode = 409;
                return response;
            }

            var userJarvisProfileApplication = mapper.Map<UserJarvisProfileApplication>(request);

            await userJarvisProfileApplicationRepository.LinkUserJarvisToApplication(userJarvisProfileApplication);

            var save = await userJarvisProfileApplicationRepository.SaveChangesAsync();

            if (!save)
            {
                response.Errors.Add(GlobalMessages.DATABASE_SAVE_FAILED);
                response.StatusCode = 500;
                return response;
            }

            response.Data = new PostLinkUserJarvisToApplicationResponse { Message = GlobalMessages.RECORD_SAVED_SUCCESSFULLY };

            return response;
        }
    }
}
