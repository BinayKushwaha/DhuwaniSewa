using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ServiceProviderVehicleDetail
    {
        public int ServiceProviderId { get; set; }
        public int VehicleDetailId { get; set; }
        public  int NumberOfVehicle { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public VehicleDetail VehicleDetail { get; set; }
    }
}
