﻿using JarvisAuth.Domain.Entities;
using JarvisAuth.Domain.Interfaces.Repositories.Base;

namespace JarvisAuth.Domain.Interfaces.Repositories
{
    public interface IJarvisRepository : IRepository
    {
        public Task CreateUserJarvis(UserJarvis userJarvis);

        public Task<bool> UserEmailExists(string email);

        public Task<UserJarvis> FindUserByEmail(string email);
    }
}
