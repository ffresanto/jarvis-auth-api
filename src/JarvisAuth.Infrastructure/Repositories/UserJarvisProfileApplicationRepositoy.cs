using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class UserJarvisProfileApplicationRepositoy : Repository, IUserJarvisProfileApplicationRepository
    {
        private readonly SqliteDbContext _context;
        public UserJarvisProfileApplicationRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task LinkUserJarvisToApplication(UserJarvisProfileApplication userJarvisProfileApplication)
        {
            await _context.UserJarvisProfileApplications.AddAsync(userJarvisProfileApplication);
        }
    }
}
