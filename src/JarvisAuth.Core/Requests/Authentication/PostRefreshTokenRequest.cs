namespace JarvisAuth.Core.Requests.Authentication
{
    public class PostRefreshTokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? ApplicationName { get; set; }
    }
}
