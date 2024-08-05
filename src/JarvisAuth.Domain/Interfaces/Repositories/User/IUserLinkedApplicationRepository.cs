using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserLinkedApplicationRepository : IRepository
    {
        public Task LinkUserToApplication(UserLinkedApplication userJarvisProfileApplication);
        public Task<bool> IsUserLinkedToApplication(Guid applicationId, Guid UserJarvisId);
    }
}
