namespace JarvisAuth.Core.Responses.Application
{
    public class GetApplicationResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
