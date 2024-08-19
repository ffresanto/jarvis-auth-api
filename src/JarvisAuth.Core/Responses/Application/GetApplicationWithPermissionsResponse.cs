namespace JarvisAuth.Core.Responses.Application
{
    public class GetApplicationWithPermissionsResponse
    {
        public Guid Id { get; set; }
        public string? Application { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
