using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Utils.CustomException
{
    public class CustomException:Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
