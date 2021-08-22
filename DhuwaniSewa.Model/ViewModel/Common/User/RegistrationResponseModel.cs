using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class RegistrationResponseModel
    {
        public int UserId { get; set; }
        public int PersonId  { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceSeekerId { get; set; }
        public bool IsServiceProvider { get; set; }
    }
}
