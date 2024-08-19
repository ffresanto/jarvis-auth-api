using JarvisAuth.Domain.Interfaces.Repositories.Application;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.Application
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {
        private readonly SqliteDbContext _context;
        public ApplicationRepository(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateApplication(Domain.Entities.Application application)
        {
            await _context.Applications.AddAsync(application);
        }

        public async Task<bool> ApplicationNameExists(string name)
        {
            return await _context.Applications.AnyAsync(u => u.Name == name);
        }

        public async Task<List<Domain.Entities.Application>> GetAllApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<bool> ApplicationIdExists(Guid id)
        {
            return await _context.Applications.AnyAsync(u => u.Id == id);
        }

        public async Task<ApplicationWithPermissions> FindApplicationWithPermissions(Guid? applicationId, string permissionName)
        {
            var sql = @"
                        SELECT 
                            a.Id,
                            a.Name as Application,
                            ap.Name as Permission
                        FROM applications_permissions ap
                        INNER JOIN applications a ON a.id = ap.application_id
                        WHERE a.id = {0} OR a.name = {1}";

            var result = await _context.Database.SqlQueryRaw<ApplicationWithPermissionResult>(sql, applicationId, permissionName).ToListAsync();

            if (result == null || !result.Any()) return null;
            
            var groupedResult = result.GroupBy(r => new { r.Id, r.Application })
                .Select(g => new ApplicationWithPermissions
                {
                    Id = g.Key.Id,
                    Application = g.Key.Application,
                    Permissions = g.Select(x => x.Permission).ToList()
                })
                .FirstOrDefault();

            return groupedResult;
        }
    }
}
