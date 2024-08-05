namespace JarvisAuth.Domain.Models
{
    public class UserLinkedApplication
    {
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
