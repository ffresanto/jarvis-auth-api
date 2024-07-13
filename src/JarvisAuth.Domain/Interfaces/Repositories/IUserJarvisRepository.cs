using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IUserJarvisRepository : IRepository
    {
        public Task CreateUserJarvis(UserJarvis userJarvis);
        public Task<bool> UserEmailExists(string email);
        public Task<UserJarvis> FindUserByEmail(string email);    }
}
