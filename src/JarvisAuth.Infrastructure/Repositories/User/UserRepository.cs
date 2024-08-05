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

        public async Task CreateUser(Domain.Entities.User userSystem)
        {
            await _context.User.AddAsync(userSystem);
        }

        public async Task<bool> UserEmailExists(string email)
        {
            return await _context.User.AnyAsync(u => u.Email == email);
        }

        public async Task<Domain.Entities.User> FindUserByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Domain.Entities.User>> GetAllUser()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<bool> UserIdExists(Guid id)
        {
            return await _context.User.AnyAsync(u => u.Id == id);
        }
    }
}
