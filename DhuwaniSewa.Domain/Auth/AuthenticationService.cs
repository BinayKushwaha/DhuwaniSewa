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
using Microsoft.EntityFrameworkCore;
using DhuwaniSewa.Utils;

namespace DhuwaniSewa.Domain
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IRepositoryService<RefreshToken, int> _refreshTokeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRepositoryService<ApplicationUsers, int> _userRepo;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly IOtpService _otpService;

        public AuthenticationService(UserManager<ApplicationUsers> userManager,
            IOptions<JwtConfiguration> setting,
            IRepositoryService<RefreshToken, int> refreshTokeRepo,
            IUnitOfWork unitOfWork,
            TokenValidationParameters tokenValidationParameters,
            IRepositoryService<ApplicationUsers, int> userRepo,
            IMailService mailService,
            IUserService userService,
            IOtpService otpService)
        {
            this._userManager = userManager;
            this._jwtConfiguration = setting.Value;
            this._refreshTokeRepo = refreshTokeRepo;
            this._unitOfWork = unitOfWork;
            this._tokenValidationParameters = tokenValidationParameters;
            this._userRepo = userRepo;
            this._mailService = mailService;
            this._userService = userService;
            this._otpService = otpService;
        }
        public async Task<ResponseModel> Login(LoginViewModel request)
        {
            try
            {
                ApplicationUsers user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    throw new CustomException("Invalid inputs.");
                if (!await _userManager.CheckPasswordAsync(user, request.Password))
                    throw new CustomException("Incorrect username or password.");
                if (!user.EmailConfirmed && !user.PhoneNumberConfirmed)
                    throw new CustomException("Account not verified. Please verify.");

                var userDetail =await _userService.GetAsync(user.Id);

                AuthenticationResult result = await CreateJwtTokenAsync(user);
                if (!result.Succeeded)
                    throw new Exception("Failed to create token");

                var responseModel = new LoginResponseModel();
                responseModel.UserId = userDetail.UserId;
                responseModel.AppUserId = userDetail.AppUserId;
                responseModel.UserName = userDetail.UserName;
                responseModel.FullName = userDetail.FullName;
                responseModel.AccessToken = result.Token;
                responseModel.RefreshToken = result.RefreshToken;
                return ResponseModel.Success("Logedin successfully.", responseModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetRefreshedTokenAsync(RefreshTokenViewModel request)
        {
            try
            {
                var token = new TokenModel()
                {
                    Token = request.AccessToken,
                    RefreshToken = request.RefreshToken
                };
                var authResponse = await RefreshToken(token);
                if (!authResponse.Succeeded)
                {
                    return ResponseModel.Error(string.Join(",", authResponse.Errors));
                }

                RefreshTokenViewModel refreshedToken = new RefreshTokenViewModel();
                refreshedToken.AccessToken = authResponse.Token;
                refreshedToken.RefreshToken = authResponse.RefreshToken;

                return ResponseModel.Success("Access token is refreshed successfully.", refreshedToken);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error("Failed to refresh access token.");
            }
        }
        
        public async Task<bool> VerifyAccountAsync(VerifyOtpViewModel request)
        {
            using (var transaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    bool verified = false;
                    var otpVerified = await _otpService.VerifyOtpAsync(request);
                    if (!otpVerified)
                        throw new CustomException("Your otp is expired. Please try another.");
                    var aspUser = await _userRepo.GetAync(a => a.UserName == request.UserName);
                    if (CustomValidator.IsEmail(request.UserName))
                    {
                        aspUser.EmailConfirmed = true;
                        verified = true;
                    }
                    else if (CustomValidator.IsMobileNumber(request.UserName))
                    {
                        aspUser.PhoneNumberConfirmed = true;
                        verified = true;
                    }
                    _userRepo.Update(aspUser);

                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();
                    return verified;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> VerifyOtpResetPassword(PasswordResetViewModel request)
        {
            using (var tranaction = await _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    bool succeeded = false;
                    request.UserName = request.UserName.Trim();
                    OtpViewModel param = new OtpViewModel()
                    {
                        UserName = request.UserName,
                        Otp = request.Otp
                    };

                    var otpVerifyModel = new VerifyOtpViewModel();
                    otpVerifyModel.UserName = request.UserName;
                    otpVerifyModel.Otp = request.Otp;

                    var otpVerified = await _otpService.VerifyOtpAsync(otpVerifyModel);
                    if (!otpVerified)
                        throw new CustomException("Your otp is expired. Please try another.");

                    var user = await _userManager.FindByNameAsync(request.UserName);
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                        throw new Exception("Failed to reset password.");

                    await tranaction.CommitAsync();
                    succeeded = result.Succeeded;
                    return succeeded;
                }
                catch (Exception ex)
                {
                    await tranaction.RollbackAsync();
                    throw;
                }
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
            catch (Exception ex)
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
                var result = await AddUpdateRefreshToken(refreshToken);
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
