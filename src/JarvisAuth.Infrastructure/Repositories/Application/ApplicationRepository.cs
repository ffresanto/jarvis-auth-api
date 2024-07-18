using JarvisAuth.Domain.Interfaces.Repositories.Application;
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
    }
}
