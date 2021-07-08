using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.DbEntities
{
    public class  SerialNumbers
    {
        public int Id { get; set; }
        public int ServiceProvider { get; set; }
        public int ServiceSeeker { get; set; }
        public int Employee { get; set; }
    }
}
