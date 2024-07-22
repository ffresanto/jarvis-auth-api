using JarvisAuth.Domain.Interfaces.Repositories.Base;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserRepository : IRepository
    {
        public Task CreateUser(Entities.User user);
    }
}
