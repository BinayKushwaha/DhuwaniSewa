using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class FiscalYearDetailViewModel 
    {
        public int FiscalYearId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
