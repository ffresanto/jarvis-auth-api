using Microsoft.EntityFrameworkCore;

namespace JarvisAuth.Infrastructure.Contexts
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
    }
 
}
