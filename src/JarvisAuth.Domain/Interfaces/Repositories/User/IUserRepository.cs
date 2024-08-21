using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.User
{
    public interface IUserRepository : IRepository
    {
        public Task CreateUser(Domain.Entities.User user);
        public Task<bool> UserEmailExists(string email);
        public Task<Domain.Entities.User> FindUserByEmail(string? email);
        public Task<List<Domain.Entities.User>> GetAllUser();
        public Task<bool> UserIdExists(Guid id);
        public Task<Domain.Entities.User> FindUserById(Guid userId);
        public Task UpdateUser(Domain.Entities.User user);
    }
}
