using AutoMapper;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GenderType, GetGenderTypeResponse>();
            CreateMap<DocumentType, GetDocumentTypeResponse>(); 
        }
    }
}
