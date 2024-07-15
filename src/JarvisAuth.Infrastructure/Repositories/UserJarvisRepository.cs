using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories
{
    public class UserJarvisRepository : Repository, IUserJarvisRepository
    {
        private readonly SqliteDbContext _context;
        public UserJarvisRepository(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateUserJarvis(UserJarvis userSystem)
        {
            await _context.UserJarvis.AddAsync(userSystem);
        }

        public async Task<bool> UserEmailExists(string email)
        {
            return await _context.UserJarvis.AnyAsync(u => u.Email == email);
        }

        public async Task<UserJarvis> FindUserByEmail(string email)
        {
            return await _context.UserJarvis.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<UserJarvis>> GetAllUserJarvis()
        {
            return await _context.UserJarvis.ToListAsync();
        }

        public async Task<bool> UserIdExists(Guid id)
        {
            return await _context.UserJarvis.AnyAsync(u => u.Id == id);
        }
    }
}
