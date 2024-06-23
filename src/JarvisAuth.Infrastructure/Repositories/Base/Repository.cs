using JarvisAuth.Domain.Interfaces.Repositories.Base;
using JarvisAuth.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Repositories.Base
{
    public abstract class Repository(SqliteDbContext context) : IRepository, IDisposable
    {
        private bool _disposed = false;
        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
