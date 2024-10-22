﻿using JarvisAuth.Domain.Interfaces.Repositories.User;
using JarvisAuth.Domain.Models;
using JarvisAuth.Infrastructure.Contexts;
using JarvisAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.User
{
    public class UserPermissionRepositoy : Repository, IUserPermissionRepository
    {
        private readonly SqliteDbContext _context;

        public UserPermissionRepositoy(SqliteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UserPermissionExistsById(Guid applicationPermissionId)
        {
            var entity = await _context.UsersPermissions.FirstOrDefaultAsync(e => e.ApplicationPermissionId == applicationPermissionId);

            if (entity == null) return false;

            return true;
        }

        public async Task AssociateUserPermission(UserPermission userPermission)
        {
            await _context.UsersPermissions.AddAsync(userPermission);
        }

        public async Task<bool> DeleteApplicationPermission(Guid userId, Guid applicationPermissionId)
        {
            var entity = await _context.UsersPermissions.FirstOrDefaultAsync(e => e.UserId == userId && e.ApplicationPermissionId == applicationPermissionId);

            if (entity == null) return false;

            _context.UsersPermissions.Remove(entity);

            return true;
        }
    }
}
