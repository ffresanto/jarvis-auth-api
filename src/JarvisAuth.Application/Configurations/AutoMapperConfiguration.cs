using AutoMapper;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<GenderType, GetGenderTypeResponse>();
            CreateMap<DocumentType, GetDocumentTypeResponse>(); 
        }
    }
}
