using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.User
{
    public class UserAssociateApplicationRepositoy : Repository, IUserAssociateApplicationRepository
    {
        private readonly SqliteDbContext _context;
        public UserAssociateApplicationRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AssociateUserToApplication(UserAssociateApplication userJarvisProfileApplication)
        {
            await _context.UserAssociateApplications.AddAsync(userJarvisProfileApplication);
        }

        public async Task<bool> IsUserAssociateToApplication(Guid applicationId, Guid UserJarvisId)
        {
            return await _context.UserAssociateApplications.AnyAsync(u => u.ApplicationId == applicationId && u.UserId == UserJarvisId);
        }
    }
}
