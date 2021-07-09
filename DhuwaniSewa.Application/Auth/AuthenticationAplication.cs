using System.Threading.Tasks;
using DhuwaniSewa.Domain.Auth;
using DhuwaniSewa.Model.ViewModel;

namespace DhuwaniSewa.Application.Auth
{
    public class AuthenticationAplication : IAuthenticationApplication
    {
        public readonly IAuthenticationService _authenticationDomain;
        public AuthenticationAplication(IAuthenticationService authenticationDomain)
        {
            _authenticationDomain = authenticationDomain;
        }
        public async Task<ResponseModel> Login(LoginViewModel request)
        {
            return await _authenticationDomain.Login(request);
        }
        public async Task<ResponseModel> GetRefreshedToken(RefreshTokenViewModel request)
        {
            return await _authenticationDomain.GetRefreshedToken(request);
        }
    }
}
