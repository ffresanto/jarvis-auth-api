using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using static System.Net.Mime.MediaTypeNames;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class ApplicationPermissionRepository : Repository, IApplicationPermissionRepository
    {
        private readonly SqliteDbContext _context;

        public ApplicationPermissionRepository(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateApplicationPermission(ApplicationPermission applicationPermission)
        {
            await _context.ApplicationPermissions.AddAsync(applicationPermission);
        }
    }
}
