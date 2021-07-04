using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Model.DbEntities
{
    public class AppUsers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsCompnay { get; set; }
        public virtual ApplicationUsers Users { get; set; }
        public virtual Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public PersonalDetail PersonalDetail { get; set; }
        public CompanyDetail CompanyDetail { get; set; }
    }
}
