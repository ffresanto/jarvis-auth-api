using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;

namespace JarvisAuth.Infrastructure.Repositories.User
{
    public class UserPermissionRepositoy : Repository, IUserPermissionRepository
    {
        private readonly SqliteDbContext _context;

        public UserPermissionRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task LinkUserPermission(UserPermission userPermission)
        {
            await _context.UsersPermissions.AddAsync(userPermission);
        }
    }
}
