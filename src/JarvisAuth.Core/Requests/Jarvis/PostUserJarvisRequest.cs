using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostUserJarvisRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public List<string> Validate(PostUserJarvisRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.Name)) errors.Add(GlobalMessages.NAME_REQUIRED);

            if (data.Name.Length < 2 || data.Name.Length > 100) errors.Add(GlobalMessages.NAME_LENGTH_2_TO_100);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Email)) errors.Add(GlobalMessages.EMAIL_REQUIRED);

            if (!GlobalValidations.IsValidEmail(data.Email)) errors.Add(GlobalMessages.INVALID_EMAIL);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Password)) errors.Add(GlobalMessages.PASSWORD_REQUIRED);

            if (data.Password.Length < 6) errors.Add(GlobalMessages.PASSWORD_MIN_LENGTH_6);

            return errors;
        }

    }
}
