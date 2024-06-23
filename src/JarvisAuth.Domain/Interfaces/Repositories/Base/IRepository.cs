namespace JarvisAuth.Domain.Interfaces.Repositories.Base
{
    public interface IRepository
    {
        public Task<bool> SaveChangesAsync();
        public void Dispose();
    }
}
