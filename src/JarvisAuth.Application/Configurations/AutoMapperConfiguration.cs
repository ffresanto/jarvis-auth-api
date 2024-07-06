using AutoMapper;
using JarvisAuth.Core.Requests.Application;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Domain.Entities;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<PostCreateUserJarvisRequest, UserJarvis>();
            CreateMap<PostCreateApplicationRequest, Domain.Entities.Application>();

        }
    }
}
