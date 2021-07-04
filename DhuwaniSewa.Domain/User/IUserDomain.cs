using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Domain
{
    public interface IUserDomain
    {
        Task<RegisterUserViewModel> Register(RegisterUserViewModel model);
    }
}
