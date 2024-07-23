using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.User
{
    public class PostLinkUserToApplicationRequest
    {
        public Guid UserJarvisId { get; set; }
        public Guid ApplicationId { get; set; }

        public List<string> Validate(PostLinkUserToApplicationRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.UserJarvisId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.ApplicationId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            return errors;
        }
    }
}
