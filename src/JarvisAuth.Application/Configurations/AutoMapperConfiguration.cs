using AutoMapper;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Requests.UserJarvis;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.UserJarvis;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<PostUserJarvisRequest, UserJarvis>();
            CreateMap<PostApplicationRequest, Domain.Entities.Application>();
            CreateMap<GetApplicationResponse, Domain.Entities.Application>();
            CreateMap<Domain.Entities.Application, GetApplicationResponse>();
            CreateMap<PostLinkUserJarvisToApplicationRequest, UserJarvisLinkedApplication>();
            CreateMap<GetUserJarvisResponse, UserJarvis>();
            CreateMap<UserJarvis, GetUserJarvisResponse>();
            CreateMap<PostApplicationPermissionRequest, ApplicationPermission>();
        }
    }
}
