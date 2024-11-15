using JarvisAuth.Domain.Entities.Base;

namespace JarvisAuth.Domain.Entities
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool Enabled { get; set; }

        public User()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsAdmin = true;
            Enabled = true;
        }
    }
}
