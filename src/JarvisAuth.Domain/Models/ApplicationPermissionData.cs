namespace JarvisAuth.Domain.Models
{
    public class ApplicationPermissionData
    {
        public Guid Id { get; set; }
        public string? Application { get; set; }
        public string? Permission { get; set; }
    }
}
