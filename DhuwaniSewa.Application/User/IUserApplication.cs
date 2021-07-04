using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Application.User
{
    public interface IUserApplication
    {
        Task<RegisterUserViewModel> Register(RegisterUserViewModel model);
    }
}
