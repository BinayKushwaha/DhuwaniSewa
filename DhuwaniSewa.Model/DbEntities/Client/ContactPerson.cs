using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ContactPerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool Active { get; set; }
        public virtual PersonalDetail PersonalDetail { get; set; }
        public virtual ServiceProviderContactPerson ServiceProviderContactPerson { get; set; }
    }
}
