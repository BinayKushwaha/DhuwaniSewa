using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils.CustomException;
using DhuwaniSewa.Utils.Helper;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace DhuwaniSewa.Domain
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IRepositoryService<RefreshToken, int> _refreshTokeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthenticationService(UserManager<ApplicationUsers> userManager,
            IOptions<JwtConfiguration> setting,
            IRepositoryService<RefreshToken, int> refreshTokeRepo,
            IUnitOfWork unitOfWork,
            TokenValidationParameters tokenValidationParameters)
        {
            this._userManager = userManager;
            this._jwtConfiguration = setting.Value;
            this._refreshTokeRepo = refreshTokeRepo;
            this._unitOfWork = unitOfWork;
            this._tokenValidationParameters = tokenValidationParameters;
        }
        public async Task<ResponseModel> Login(LoginViewModel request)
        {
            try
            {
                ApplicationUsers user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    throw new CustomException("Invalid inputs.");
                if (!await _userManager.CheckPasswordAsync(user, request.Password))
                    throw new CustomException("Username and password are not correct.");
                  
                AuthenticationResult result = await CreateJwtTokenAsync(user);
                if (!result.Succeeded)
                    throw new Exception("Failed to create token"); 
                var token = new RefreshTokenViewModel();
                token.AccessToken = result.Token;
                token.RefreshToken = result.RefreshToken;
                return ResponseModel.Success("Logedin successfully.",token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ResponseModel> GetRefreshedToken(RefreshTokenViewModel request)
        {
            try 
            {
                var token = new TokenModel() { 
                Token=request.AccessToken,
                RefreshToken=request.RefreshToken
                };
                var authResponse = await RefreshToken(token);
                if (!authResponse.Succeeded)
                {
                    return ResponseModel.Error(string.Join(",", authResponse.Errors));
                }

                RefreshTokenViewModel refreshedToken = new RefreshTokenViewModel();
                refreshedToken.AccessToken = authResponse.Token;
                refreshedToken.RefreshToken = authResponse.RefreshToken;

                return ResponseModel.Success("Access token is refreshed successfully.",refreshedToken);
            }
            catch(Exception ex)
            {
               return ResponseModel.Error("Failed to refresh access token.");
            }
        }

        
        #region Helper Methods
        private async Task<AuthenticationResult> AddUpdateRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                var existingRefreshToken = await _refreshTokeRepo.GetAync(a => a.UserId == refreshToken.UserId);
                if (existingRefreshToken != null)
                    _refreshTokeRepo.Delete(existingRefreshToken);
                var result = await _refreshTokeRepo.AddAsync(refreshToken);
                await _unitOfWork.CommitAsync();
                return new AuthenticationResult
                {
                    RefreshToken = refreshToken.Token,
                    Succeeded = true
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private async Task<AuthenticationResult> RefreshToken(TokenModel model)
        {
            try
            {
                var validatedToken = GetPrincipalFromToken(model.Token);
                if (validatedToken == null)
                {
                    return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
                }

                var expiryDateUnix =
                    long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryDateUnix);

                if (expiryDateTimeUtc > DateTime.UtcNow)
                {
                    return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
                }

                var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                var storedRefreshToken = await _refreshTokeRepo.GetAync(x => x.Token == model.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
                }

                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
                }

                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
                }

                string userId = validatedToken.Claims.Single(x => x.Type == "UserId").Value;
                ApplicationUsers applicationUser = await _userManager.FindByIdAsync(userId);
                if (applicationUser == null)
                    return new AuthenticationResult { Errors = new[] { "User Not Found" } };
                return await CreateJwtTokenAsync(applicationUser);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                throw;
            }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
        private async Task<AuthenticationResult> CreateJwtTokenAsync(ApplicationUsers user)
        {
            try
            {
                AuthenticationResult authenticationResult = new AuthenticationResult();

                var userRoles = await _userManager.GetRolesAsync(user);
                var roleClaims = new List<Claim>();
                foreach (var role in userRoles)
                {
                    roleClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                ClaimsIdentity subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserId", user.Id),
                }
                .Union(roleClaims));

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfiguration.JwtSetting.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.Add(_jwtConfiguration.JwtSetting.TokenLifeSpan),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                authenticationResult.Token = tokenHandler.WriteToken(token);

                var refreshToken = new RefreshToken
                {
                    Token = HashMeHelper.Get(Guid.NewGuid().ToString()),
                    JwtId = token.Id,
                    UserId = user.Id,
                    CreationDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMonths(6)
                };
                var result=await AddUpdateRefreshToken(refreshToken);
                authenticationResult.RefreshToken = result.RefreshToken;
                authenticationResult.Succeeded = true;
                return authenticationResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // use this method for revoking access token while refreshing token and logout  .
        private async Task RemoveAccessToken(string refreshToken)
        {
            try
            {
                var result = await _refreshTokeRepo.GetAync(a => a.Token == refreshToken);
                if (result == null)
                    throw new CustomException($"{refreshToken} does not exists.");
                _refreshTokeRepo.Delete(result);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
//TO DO: 1. Get Host Ip. 2. Request model implementation with Ip, UserId, RequestedDate. 3.Modified record implementation 
