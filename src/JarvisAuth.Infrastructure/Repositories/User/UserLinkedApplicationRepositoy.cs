using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.User
{
    public class UserLinkedApplicationRepositoy : Repository, IUserLinkedApplicationRepository
    {
        private readonly SqliteDbContext _context;
        public UserLinkedApplicationRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task LinkUserToApplication(UserLinkedApplication userJarvisProfileApplication)
        {
            await _context.UserLinkedApplications.AddAsync(userJarvisProfileApplication);
        }

        public async Task<bool> IsUserLinkedToApplication(Guid applicationId, Guid UserJarvisId)
        {
            return await _context.UserLinkedApplications.AnyAsync(u => u.ApplicationId == applicationId && u.UserId == UserJarvisId);
        }
    }
}
