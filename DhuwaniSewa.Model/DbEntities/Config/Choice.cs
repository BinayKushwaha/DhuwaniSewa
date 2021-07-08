using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class Choice:RecordHistory
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Enum { get; set; }
        public string DisplayName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IList<VehicleDetail> VehicleDetailType { get; set; }
        public IList<VehicleDetail> VehicleDetailBrand { get; set; }
    }
}
