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
        public string Model { get; set; }
        public int MaxWeight { get; set; }
        public int WeightUnit { get; set; }
        public int WheelType { get; set; }
        public ServiceProviderVehicleDetail ServiceProviderVehicleDetail { get; set; }
        public Choice Type { get; set; }//container, truck, mini truck, 
        public Choice Brand { get; set; }
    }
}
