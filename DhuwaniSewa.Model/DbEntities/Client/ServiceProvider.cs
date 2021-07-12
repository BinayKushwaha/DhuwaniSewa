using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ServiceProvider:RecordHistory
    {
        public ServiceProvider()
        {
            ServiceProviderVehicleDetail = new HashSet<ServiceProviderVehicleDetail>();
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool DetailsCorrectAgreed { get; set; }
        public int UserId { get; set; }
        public string DhuwaniSewaId { get; set; }
        public virtual AppUsers AppUser { get; set; }
        public virtual ICollection<ServiceProviderVehicleDetail> ServiceProviderVehicleDetail { get; set; }
    }
}
