using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IApplicationPermissionRepository : IRepository
    {
        public Task CreateApplicationPermission(ApplicationPermission applicationPermission);
    }
}
