using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.Application
{
    public interface IApplicationRepository : IRepository
    {
        public Task CreateApplication(Entities.Application application);
        public Task<bool> ApplicationNameExists(string name);
        public Task<List<Entities.Application>> GetAllApplications();
        public Task<bool> ApplicationIdExists(Guid id);
        public Task<ApplicationWithPermissions> FindApplicationWithPermissions(Guid? applicationId, string permissionName);
        public Task<Entities.Application> FindApplicationById(Guid applicationId);
        public Task UpdateApplication(Entities.Application application);
        public Task<List<PermissionApplication>> FindApplicationWithListPermissions(Guid? applicationId, string permissionName);
    }
}
