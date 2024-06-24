using AutoMapper;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<PostCreateUserJarvisRequest, UserJarvis>();
        }
    }
}
