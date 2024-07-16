using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class UserJarvisLinkedApplicationRepositoy : Repository, IUserJarvisLinkedApplicationRepository
    {
        private readonly SqliteDbContext _context;
        public UserJarvisLinkedApplicationRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task LinkUserJarvisToApplication(UserJarvisLinkedApplication userJarvisProfileApplication)
        {
            await _context.UserJarvisLinkedApplications.AddAsync(userJarvisProfileApplication);
        }

        public async Task<bool> IsUserLinkedToApplication(Guid applicationId, Guid UserJarvisId)
        {
            return await _context.UserJarvisLinkedApplications.AnyAsync(u => u.ApplicationId == applicationId && u.UserJarvisId == UserJarvisId);
        }
    }
}
