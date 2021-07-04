using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class AuthenticationResult : TokenModel
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
