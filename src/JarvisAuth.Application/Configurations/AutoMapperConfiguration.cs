using AutoMapper;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Application;
using JarvisAuth.Core.Responses.User;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<PostUserRequest, User>();
            CreateMap<PostApplicationRequest, Domain.Entities.Application>();
            CreateMap<GetApplicationResponse, Domain.Entities.Application>();
            CreateMap<Domain.Entities.Application, GetApplicationResponse>();
            CreateMap<PostLinkUserToApplicationRequest, UserLinkedApplication>();
            CreateMap<GetUserResponse, User>();
            CreateMap<User, GetUserResponse>();
            CreateMap<PostApplicationPermissionRequest, ApplicationPermission>();
            CreateMap<PostUserPermissionRequest, UserPermission>();
            CreateMap<GetApplicationWithPermissionsResponse, ApplicationPermissionData>();
            CreateMap<ApplicationPermissionData, GetApplicationWithPermissionsResponse>();
        }
    }
}
