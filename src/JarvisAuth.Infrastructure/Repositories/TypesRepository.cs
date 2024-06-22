using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class TypesRepository(SqliteDbContext context) : ITypesRepository
    {
        public async Task<List<DocumentType>> GetDocumentTypes()
        {
            return await context.DocumentTypes.ToListAsync();
        }

        public async Task<List<GenderType>> GetGenderTypes()
        {
            return await context.GenderTypes.ToListAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
