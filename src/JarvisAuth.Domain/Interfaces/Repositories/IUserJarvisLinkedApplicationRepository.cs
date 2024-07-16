using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IUserJarvisLinkedApplicationRepository : IRepository
    {
        public Task LinkUserJarvisToApplication(UserJarvisLinkedApplication userJarvisProfileApplication);
        public Task<bool> IsUserLinkedToApplication(Guid applicationId, Guid UserJarvisId);
    }
}
