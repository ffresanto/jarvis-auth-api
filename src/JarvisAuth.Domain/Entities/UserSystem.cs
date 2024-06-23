using JarvisAuth.Domain.Entities.Base;

namespace JarvisAuth.Domain.Entities
{
    public class UserSystem : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int ContactNumber { get; set; }
        public int GenderTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserSystemRoleId { get; set; }
        public bool Enabled { get; set; }

        public UserSystem()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
