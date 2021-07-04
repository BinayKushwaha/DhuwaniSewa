using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class JwtConfiguration
    {
        public JwtSetting JwtSetting { get; set; }
    }
    public class JwtSetting
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeSpan { get; set; }
    }
}
