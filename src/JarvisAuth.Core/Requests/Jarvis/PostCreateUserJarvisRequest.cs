using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostCreateUserJarvisRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public List<string> Validate(PostCreateUserJarvisRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.Name)) errors.Add("Name is required.");

            if (data.Name.Length < 2 || data.Name.Length > 100) errors.Add("Name must be between 2 and 100 characters.");

            if (GlobalValidations.IsNullOrEmptyCustom(data.Email)) errors.Add("Email is required.");

            if (!GlobalValidations.IsValidEmail(data.Email)) errors.Add("Invalid email format.");

            if (GlobalValidations.IsNullOrEmptyCustom(data.Password)) errors.Add("Password is required.");

            if (data.Password.Length < 6) errors.Add("Password must be at least 6 characters long.");

            return errors;
        }

    }
}
