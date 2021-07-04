using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Application.Auth
{
    public interface IAuthenticationApplication
    {
        Task<ResponseModel> Login(LoginViewModel request);
        Task<ResponseModel> GetRefreshedToken(RefreshTokenViewModel request);
    }
}
