using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;
using Newtonsoft.Json;

namespace JarvisAuth.Core.Requests.User
{
    public class PostUserPermissionRequest
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("applicationPermissionId")]
        public Guid ApplicationPermissionId { get; set; }

        public List<string> Validate(PostUserPermissionRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.UserId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            if (GlobalValidations.IsNullOrEmptyCustom(data.ApplicationPermissionId.ToString())) errors.Add(GlobalMessages.NAME_REQUIRED);

            return errors;
        }
    }
}
