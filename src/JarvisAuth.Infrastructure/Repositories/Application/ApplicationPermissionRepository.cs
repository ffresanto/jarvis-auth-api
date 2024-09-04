using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.Application
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
        public async Task<bool> ApplicationPermissionNameExists(string name)
        {
            return await _context.ApplicationPermissions.AnyAsync(u => u.Name == name);
        }

        public async Task<bool> DeleteApplicationPermission(Guid permissionId)
        {
            var entity = await _context.ApplicationPermissions.FirstOrDefaultAsync(e => e.Id == permissionId);

            if (entity == null) return false;

            _context.ApplicationPermissions.Remove(entity);

            return true;
        }
    }
}
