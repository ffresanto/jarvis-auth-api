namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostRefreshTokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
