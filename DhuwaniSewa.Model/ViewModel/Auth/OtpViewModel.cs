using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class OtpViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmalMobileNumber { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string Otp { get; set; }
    }
}
