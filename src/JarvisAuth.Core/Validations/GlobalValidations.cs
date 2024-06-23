using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JarvisAuth.Core.Validations
{
    public class GlobalValidations
    {
        public static bool IsNullOrEmptyCustom(string? field)
        {
            return (string.IsNullOrEmpty(field) || field.ToLower() == "string");
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
