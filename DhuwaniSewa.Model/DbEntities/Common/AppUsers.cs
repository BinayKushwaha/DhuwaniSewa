using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.DbEntities;

namespace DhuwaniSewa.Model.DbEntities
{
    public class AppUsers:RecordHistory
    {
        public AppUsers()
        {
            ServiceProvider = new HashSet<ServiceProvider>();
            ServiceSeeker = new HashSet<ServiceSeeker>();
            Employee = new HashSet<Employee>();
            CompanyDetail = new HashSet<CompanyDetail>();
            PersonalDetail = new HashSet<PersonalDetail>();
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public string UserId { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsCompnay { get; set; }
        public bool IsServiceProvider { get; set; }

        public virtual ApplicationUsers Users { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<PersonalDetail> PersonalDetail { get; set; }
        public virtual ICollection<CompanyDetail> CompanyDetail { get; set; }
        public virtual ICollection<ServiceProvider> ServiceProvider { get; set; }
        public virtual ICollection<ServiceSeeker> ServiceSeeker { get; set; }
    }
}
