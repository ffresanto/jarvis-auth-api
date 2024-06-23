using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface ISystemRepository : IRepository
    {
        public Task<List<GenderType>> GetGenderTypes();
        public Task<List<DocumentType>> GetDocumentTypes();
    }
}
