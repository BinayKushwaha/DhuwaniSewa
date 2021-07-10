using DhuwaniSewa.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface ISerialNumberSevice
    {
        Task<int> Get(SerialNumber type);
    }
}
