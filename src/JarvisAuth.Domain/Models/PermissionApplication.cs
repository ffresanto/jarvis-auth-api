namespace JarvisAuth.Domain.Models
{
    public class PermissionApplication
    {
        public Guid Id { get; set; }
        public string? Application { get; set; }
        public string? Permission { get; set; }
    }
}
