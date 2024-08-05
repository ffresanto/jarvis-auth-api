namespace JarvisAuth.Domain.Models
{
    public class UserPermission
    {
        public Guid UserId { get; set; }
        public Guid ApplicationPermissionId { get; set; }
    }
}
