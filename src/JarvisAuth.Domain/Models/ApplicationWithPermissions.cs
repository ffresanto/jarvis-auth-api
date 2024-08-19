namespace JarvisAuth.Domain.Models
{
    public class ApplicationWithPermissions
    {
        public Guid Id { get; set; }
        public string? Application { get; set; }
        public List<string>? Permissions { get; set; }
    }

    #region Helper Class
    public class ApplicationWithPermissionResult
    {
        public Guid Id { get; set; }
        public string? Application { get; set; }
        public string? Permission { get; set; }
    }

    #endregion
}
