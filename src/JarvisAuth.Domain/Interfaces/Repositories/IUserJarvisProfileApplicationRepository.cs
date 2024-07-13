using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IUserJarvisProfileApplicationRepository : IRepository
    {
        public Task LinkUserJarvisToApplication(UserJarvisProfileApplication userJarvisProfileApplication);
    }
}
