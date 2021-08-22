using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public bool IsServiceProvider { get; set; }
        public string FullName { get; set; }
        public int PersonId { get; set; }
    }
}
