using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.User
{
    public class DeleteUserPermissionRequest
    {
        public Guid UserId { get; set; }
        public Guid ApplicationPermissionId { get; set; }

        public List<string> Validate(DeleteUserPermissionRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.UserId.ToString())) errors.Add(GlobalMessages.USER_ID_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.ApplicationPermissionId.ToString())) errors.Add(GlobalMessages.PERMISSION_ID_REQUIRED);

            return errors;
        }
    }
}
