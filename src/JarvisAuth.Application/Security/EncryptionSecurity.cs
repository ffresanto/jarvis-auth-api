namespace JarvisAuth.Application.Security
{
    public static class EncryptionSecurity
    {
        public static string EncryptPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPasswordEncryption(string? password, string? hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
