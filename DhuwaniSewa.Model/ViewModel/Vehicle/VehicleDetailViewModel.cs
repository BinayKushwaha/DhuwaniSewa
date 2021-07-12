using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class VehicleDetailViewModel
    {
        public int VehicleDetailId { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        public string RegistrationNumber { get; set; }
        public int MaxWeight { get; set; }
        public int WeightUnit { get; set; }
        public int WeightUnitDisplayName { get; set; }
        public int WheelType { get; set; }
        public int WheelTypeDisplayName { get; set; }
        public string Model { get; set; }

    }
}
