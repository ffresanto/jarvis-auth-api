using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.Application
{
    public class PostApplicationPermissionRequest
    {
        public Guid ApplicationId { get; set; }
        public string? Name { get; set; }

        public List<string> Validate(PostApplicationPermissionRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.ApplicationId.ToString())) errors.Add(GlobalMessages.APPLICATION_ID_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Name)) errors.Add(GlobalMessages.NAME_REQUIRED);

            return errors;
        }
    }
}
