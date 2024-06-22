﻿using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;

namespace JarvisAuth.Domain.Interfaces.Services
{
    public interface ITypeService
    {
        public Task<Response<List<GetGenderTypeResponse>>> GetGendersTypes();
        public Task<Response<List<GetDocumentTypeResponse>>> GetDocumentsTypes();
    }
}