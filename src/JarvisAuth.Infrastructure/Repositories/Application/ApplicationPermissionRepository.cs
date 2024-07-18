using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
    }
}
