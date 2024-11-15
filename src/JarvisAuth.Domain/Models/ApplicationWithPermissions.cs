namespace JarvisAuth.Domain.Models
{
    public class ApplicationWithPermissions
    {
        public Guid Id { get; set; }
        public string? Application { get; set; } = string.Empty;
        public List<string>? Permissions { get; set; } = new List<string>();
    }
}
