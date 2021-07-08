using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Model.DbEntities
{
    public class AppUsers:RecordHistory
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string UserId { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsCompnay { get; set; }
        public bool IsServiceProvider { get; set; }

        public ApplicationUsers Users { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public PersonalDetail PersonalDetail { get; set; }
        public CompanyDetail CompanyDetail { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public ServiceSeeker ServiceSeeker { get; set; }
    }
}
