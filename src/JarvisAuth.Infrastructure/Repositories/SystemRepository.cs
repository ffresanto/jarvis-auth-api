using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class SystemRepository : Repository, ISystemRepository
    {
        private readonly SqliteDbContext _context;
        public SystemRepository(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateUserSystem(UserSystem userSystem)
        {
            await _context.UserSystems.AddAsync(userSystem);
        }

        public async Task<List<DocumentType>> GetDocumentTypes()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<List<GenderType>> GetGenderTypes()
        {
            return await _context.GenderTypes.ToListAsync();
        }
    }
}
