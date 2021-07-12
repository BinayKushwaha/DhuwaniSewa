using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ContactDetail
    {
        public ContactDetail()
        {
            PersonalDetailContactDetails = new HashSet<PersonalDetailContactDetail>();
        }
        public int Id { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public ICollection<PersonalDetailContactDetail> PersonalDetailContactDetails { get; set; }
    }
}
