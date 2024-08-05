using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserPermissionRepository : IRepository
    {
        public Task LinkUserPermission(UserPermission userPermission);
    }
}
