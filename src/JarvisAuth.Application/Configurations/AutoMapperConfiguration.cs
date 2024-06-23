using AutoMapper;
using JarvisAuth.Core.Requests.System;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<GenderType, GetGenderTypeResponse>();
            CreateMap<DocumentType, GetDocumentTypeResponse>();
            CreateMap<PostCreateUserSystemRequest, UserSystem>();
        }
    }
}
