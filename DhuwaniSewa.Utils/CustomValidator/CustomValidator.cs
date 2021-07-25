using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DhuwaniSewa.Utils
{
    public static class CustomValidator
    {
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        public static bool IsMobileNumber(string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber, @"^[0-9]{10}$");
        }
    }
}
