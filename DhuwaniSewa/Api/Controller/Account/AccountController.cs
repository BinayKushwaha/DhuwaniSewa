using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils.CustomException;
using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.Constant;

namespace DhuwaniSewa.Web.Api.Controller.Account
{
    [Route("api/account")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IUserService userService,
            IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                var result = await _userService.RegisterAsync(request);
                return Ok(ResponseModel.Success("User registered successfully.", result));
            }
            catch (CustomException ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ResponseModel.Info(ex.Message, request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs");
                var result = await _authenticationService.Login(request);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return Ok(ResponseModel.Info(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                var result = await _authenticationService.GetRefreshedTokenAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registrationcreateotp")]
        public async Task<IActionResult> CreateSendRegistrationOtpAsync(RegistrationCreateOtpViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");

                var param = new OtpViewModel();
                param.UserName = request.UserName;
                param.MailSubject = MessageTemplate.Registration_OTP_Mail_Subject;
                param.MailBody = MessageTemplate.Registration_OTP_Mail_Body;
                var result = await _authenticationService.GenerateSendRegistrationOtpAsync(param);
                if (result)
                    return Ok(ResponseModel.Success("Otp is send to your username."));
                else
                    return Ok(ResponseModel.Info("Failed to create otp."));
            }
            catch (CustomException ex)
            {
                return Ok(ResponseModel.Info(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("verifyaccount")]
        public async Task<IActionResult> VerifyAccountAsync(VerifyOtpViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                var result = await _authenticationService.VerifyAccountAsync(request);
                if (result)
                    return Ok(ResponseModel.Success("Account verified succesflly."));
                else
                    return Ok(ResponseModel.Info("Your otp is expired."));
            }
            catch (CustomException ex)
            {
                return Ok(ResponseModel.Info(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetpasswordcreateotp")]
        public async Task<IActionResult>CreateSendResetPasswordOtpAsync(PasswordResetCreateOtpViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");

                var paramModel = new OtpViewModel();
                paramModel.EmalMobileNumber = request.EmailMobileNumber;
                paramModel.MailSubject = MessageTemplate.Password_Reset_OTP_Mail_Subject;
                paramModel.MailBody = MessageTemplate.Password_Reset_OTP_Mail_Body;
                var userName = await _authenticationService.GenerateSendPasswordResetOtpAsync(paramModel);
                return Ok(ResponseModel.Success("Otp is send to your username.",userName));
            }
            catch (CustomException ex)
            {
                return Ok(ResponseModel.Info(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("verifyotpresetpassword")]
        public async Task<IActionResult> VerifyOtpResetPassword(PasswordResetViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                var result = await _authenticationService.VerifyOtpResetPassword(request);
                if (result)
                    return Ok(ResponseModel.Success("Password changed successfully."));
                else
                    return Ok(ResponseModel.Info("Your otp is expired."));
            }
            catch (CustomException ex)
            {
                return Ok(ResponseModel.Info(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }
    }
}
