using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserPermissionRepository : IRepository
    {
        public Task<bool> UserPermissionExistsById(Guid applicationPermissionId);
        public Task AssociateUserPermission(UserPermission userPermission);
        public Task<bool> DeleteApplicationPermission(Guid userId, Guid applicationPermissionId);
    }
}
