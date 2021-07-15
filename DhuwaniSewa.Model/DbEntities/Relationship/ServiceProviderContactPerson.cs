using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ServiceProviderContactPerson
    {
        public int ServiceProviderId { get; set; }
        public int ContactPersonId { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
    }
}
