using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.User
{
    public class UserRepository : Repository, IUserRepository
    {
        private readonly SqliteDbContext _context;

        public UserRepository(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateUser(Domain.Entities.User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> UserEmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
