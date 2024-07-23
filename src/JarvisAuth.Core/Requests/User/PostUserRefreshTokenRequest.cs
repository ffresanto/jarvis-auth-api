namespace JarvisAuth.Core.Requests.User
{
    public class PostUserRefreshTokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
