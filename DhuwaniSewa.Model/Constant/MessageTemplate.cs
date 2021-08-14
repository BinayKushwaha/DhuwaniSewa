using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.Constant
{
    public static class MessageTemplate
    {
        //Registration
        public static string Registration_OTP_Mail_Subject = "DhuwaniSewa | Verify your account with OTP";
        public static string Registration_OTP_Mail_Body = "You have successfully registered. </br> Your OTP is <strong> {0} </strong>. Please verify your account asap.";

        //Reset password
        public static string Password_Reset_OTP_Mail_Subject = "DhuwaniSewa | Reset your account password with OTP";
        public static string Password_Reset_OTP_Mail_Body = "Your reset password OTP is <strong> {0} </strong>. Please use this OTP to reset your account asap.";

    }
}
