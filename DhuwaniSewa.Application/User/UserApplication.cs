using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Application.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserService _userDomain;
        public UserApplication(IUserService userDomain)
        {
            _userDomain = userDomain;
        }

        public async Task<RegisterUserViewModel> Register(RegisterUserViewModel model)
        {
            try
            {
                return await _userDomain.Register(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
