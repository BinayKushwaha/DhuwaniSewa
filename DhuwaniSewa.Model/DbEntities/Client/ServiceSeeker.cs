using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class ServiceSeeker:RecordHistory
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool DetailsCorrectAgreed { get; set; }
        public int UserId { get; set; }
        public string DhuwaniSewaId { get; set; }
        public AppUsers AppUser { get; set; }
    }
}
