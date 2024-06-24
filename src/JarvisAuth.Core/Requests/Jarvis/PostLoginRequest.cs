using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAuth.Core.Requests.Jarvis
{
    public class PostLoginRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }

        public List<string> Validate(PostLoginRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.Email)) errors.Add("It is mandatory to inform the email of the user.");

            if (GlobalValidations.IsNullOrEmptyCustom(data.Password)) errors.Add("It is mandatory to inform the user's password.");

            if (!GlobalValidations.IsNullOrEmptyCustom(data.Email))
            {
                if (!GlobalValidations.IsValidEmail(data.Email)) errors.Add(GlobalMessages.EMAIL_INVALID);
            }

            return errors;
        }
    }
}
