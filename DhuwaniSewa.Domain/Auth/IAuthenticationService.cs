using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Domain
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> Login(LoginViewModel request);
        Task<ResponseModel> GetRefreshedTokenAsync(RefreshTokenViewModel request);
        Task<bool> VerifyAccountAsync(VerifyOtpViewModel request);
        Task<bool> VerifyOtpResetPassword(PasswordResetViewModel request);
    }
}
