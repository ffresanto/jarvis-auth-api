using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IJarvisRepository : IRepository
    {
        public Task CreateUserJarvis(UserJarvis userJarvis);
        public Task<List<GenderType>> GetGenderTypes();
        public Task<List<DocumentType>> GetDocumentTypes();
        public Task<bool> EmailExistsAsync(string email);
    }
}
