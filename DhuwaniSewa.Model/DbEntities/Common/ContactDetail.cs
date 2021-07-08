using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public IList<PersonalDetailContactDetail> PersonalDetailContactDetails { get; set; }
    }
}
