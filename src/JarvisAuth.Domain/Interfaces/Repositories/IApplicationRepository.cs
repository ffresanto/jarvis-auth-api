using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IApplicationRepository : IRepository
    {
        public Task CreateApplication(Application application);
        public Task<bool> ApplicationNameExists(string name);
        public Task<List<Application>> GetApplications();
    }
}
