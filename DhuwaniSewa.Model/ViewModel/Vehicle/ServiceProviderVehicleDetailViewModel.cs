using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class ServiceProviderVehicleDetailViewModel
    {
        public int ServiceProviderVehicleId { get; set; }
        public int ServiceProviderId { get; set; }
        public int VehicleId { get; set; }
        public VehicleDetailViewModel VehicleDetail { get; set; }
    }
}
