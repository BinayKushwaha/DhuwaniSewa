using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class OtpViewModel
    {
        public string UserName { get; set; }
        public string EmalMobileNumber { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string Otp { get; set; }
    }
    public class RegistrationCreateOtpViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
    public class PasswordResetCreateOtpViewModel
    {
        [Required]
        public string EmailMobileNumber { get; set; }
    }
    public class VerifyOtpViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Otp { get; set; }
    }
}
