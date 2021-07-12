using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class PersonalDetailContactDetail
    {
        public int PersonalDetailId { get; set; }
        public int ContactDetailId { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }
    }
}
