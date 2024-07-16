using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostJarvisLoginRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }

        public List<string> Validate(PostJarvisLoginRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.Email)) errors.Add(GlobalMessages.MANDATORY_EMAIL);

            if (GlobalValidations.IsNullOrEmptyCustom(data.Password)) errors.Add(GlobalMessages.MANDATORY_PASSWORD);

            if (!GlobalValidations.IsNullOrEmptyCustom(data.Email))
            {
                if (!GlobalValidations.IsValidEmail(data.Email)) errors.Add(GlobalMessages.INVALID_EMAIL);
            }

            return errors;
        }
    }
}
