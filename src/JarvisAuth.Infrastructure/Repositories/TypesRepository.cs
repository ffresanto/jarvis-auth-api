using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class TypesRepository(SqliteDbContext context) : ITypesRepository
    {
        public async Task<List<DocumentType>> GetDocumentsTypes()
        {
            return await context.DocumentsTypes.ToListAsync();
        }

        public async Task<List<GenderType>> GetGendersTypes()
        {
            return await context.GendersTypes.ToListAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
