using JarvisAuth.Domain.Entities.Base;

namespace JarvisAuth.Domain.Entities
{
    public class ApplicationPermission : Entity
    {
        public Guid ApplicationId { get; set; }
        public string? Name { get; set; }
    }
}
