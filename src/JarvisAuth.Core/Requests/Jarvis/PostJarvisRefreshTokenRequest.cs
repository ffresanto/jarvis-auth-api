namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostJarvisRefreshTokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
