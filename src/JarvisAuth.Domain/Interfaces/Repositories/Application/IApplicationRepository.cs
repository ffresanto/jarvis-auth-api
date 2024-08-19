﻿using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Domain.Models;

namespace JarvisAuth.Domain.Interfaces.Repositories.Application
{
    public interface IApplicationRepository : IRepository
    {
        public Task CreateApplication(Domain.Entities.Application application);
        public Task<bool> ApplicationNameExists(string name);
        public Task<List<Domain.Entities.Application>> GetAllApplications();
        public Task<bool> ApplicationIdExists(Guid id);
        public Task<ApplicationWithPermissions> FindApplicationWithPermissions(Guid? applicationId, string permissionName);
    }
}
