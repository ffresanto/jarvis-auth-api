﻿using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface ITypesRepository : IRepository
    {
        public Task<List<GenderType>> GetGendersTypes();
        public Task<List<DocumentType>> GetDocumentsTypes();
    }
}
