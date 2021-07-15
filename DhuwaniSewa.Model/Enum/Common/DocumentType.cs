using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.Enum
{
    public enum DocumentType
    {
        [Display(Name="Cititzenship")]
        Ctitzenship=1,
        [Display(Name ="Blue Book")]
        BlueBook=2
    }
}
