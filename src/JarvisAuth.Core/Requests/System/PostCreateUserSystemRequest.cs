namespace JarvisAuth.Core.Requests.System
{
    public class PostCreateUserSystemRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int ContactNumber { get; set; }
        public int GenderTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string? DocumentNumber { get; set; }
    }
}
