using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ServiceProvider:RecordHistory
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool DetailsCorrectAgreed { get; set; }
        public int UserId { get; set; }
        public string DhuwaniSewaId { get; set; }
        public AppUsers AppUser { get; set; }
        public IList<ServiceProviderVehicleDetail> ServiceProviderVehicleDetail { get; set; }
    }
}
