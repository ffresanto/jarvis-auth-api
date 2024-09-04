using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.User
{
    public class PatchUserRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Validate(PatchUserRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.UserId.ToString())) errors.Add(GlobalMessages.USER_ID_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Name)) errors.Add(GlobalMessages.NAME_REQUIRED);

            if (data.Name.Length < 2 || data.Name.Length > 100) errors.Add(GlobalMessages.NAME_LENGTH_2_TO_100);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Email)) errors.Add(GlobalMessages.EMAIL_REQUIRED);

            if (!GlobalValidations.IsValidEmail(data.Email)) errors.Add(GlobalMessages.INVALID_EMAIL);

            return errors;
        }
    }
}
