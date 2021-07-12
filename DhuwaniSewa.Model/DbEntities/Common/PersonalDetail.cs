using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetail
    {
        public PersonalDetail()
        {
            PersonalDetailDocumentDetails = new HashSet<PersonalDetailDocumentDetail>();
            PersonalDetailContactDetails = new HashSet<PersonalDetailContactDetail>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUsers  AppUsers { get; set; }
        public virtual ICollection<PersonalDetailDocumentDetail> PersonalDetailDocumentDetails { get; set; }
        public virtual ICollection<PersonalDetailContactDetail> PersonalDetailContactDetails { get; set; }
    }
}
