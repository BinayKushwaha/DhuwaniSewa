using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class VehicleDetail
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        public string RegistrationNumber { get; set; }
        public ServiceProviderVehicleDetail ServiceProviderVehicleDetail { get; set; }
        public Choice Type { get; set; }
        public Choice Brand { get; set; }
    }
}
