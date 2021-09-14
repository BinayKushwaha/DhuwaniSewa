using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class UserProfileViewModel
    {
        public int AppUserId { get; set; }
        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.FirstName) && !string.IsNullOrEmpty(this.LastName))
                    return $"{this.FirstName} {this.LastName}";
                else return string.Empty;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public string CitizenshipNumber { get; set; }
    }
}
