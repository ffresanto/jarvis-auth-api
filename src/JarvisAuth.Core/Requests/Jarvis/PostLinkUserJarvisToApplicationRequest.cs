using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.UserJarvis
{
    public class PostLinkUserJarvisToApplicationRequest
    {
        public Guid UserJarvisId { get; set; }
        public Guid ApplicationId { get; set; }

        public List<string> Validate(PostLinkUserJarvisToApplicationRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.UserJarvisId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.ApplicationId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            return errors;
        }
    }
}
