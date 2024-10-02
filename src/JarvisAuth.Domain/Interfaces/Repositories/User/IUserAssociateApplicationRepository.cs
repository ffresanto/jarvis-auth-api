using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserAssociateApplicationRepository : IRepository
    {
        public Task AssociateUserToApplication(UserAssociateApplication userJarvisProfileApplication);
        public Task<bool> IsUserAssociateToApplication(Guid applicationId, Guid UserJarvisId);
    }
}
