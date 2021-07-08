using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class Employee:RecordHistory
    {
        public int Id { get; set; }
        public string Desigination { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get;set; }
        public string DhuwaniSewaId { get; set; }
        public AppUsers AppUsers { get; set; }
    }
}
