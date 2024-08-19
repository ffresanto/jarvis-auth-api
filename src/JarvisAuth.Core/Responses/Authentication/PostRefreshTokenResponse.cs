namespace JarvisAuth.Core.Responses.Authentication
{
    public class PostRefreshTokenResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
