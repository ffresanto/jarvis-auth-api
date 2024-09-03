using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;

namespace JarvisAuth.Domain.Interfaces.Repositories.Application
{
    public interface IApplicationPermissionRepository : IRepository
    {
        public Task CreateApplicationPermission(ApplicationPermission applicationPermission);
        public Task<bool> ApplicationPermissionNameExists(string? name);
        public Task<bool> DeleteApplicationPermission(Guid idPermission);
    }
}
