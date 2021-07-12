using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.Enum
{
    public enum VehicleWheelType
    {
        [Display(Name ="4 Wheeler")]
        FourWheeler=4,
        [Display(Name ="6 Wheeler")]
        SixWheeler=6,
        [Display(Name ="10 Wheeler")]
        TenWheeler=10,
        [Display(Name ="12 Wheeler")]
        TweleveWheeler=12
    }
}
