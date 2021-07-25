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
        [Route("createotp")]
        public async Task<IActionResult> CreateSendRegistrationOtpAsync(OtpViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                request.MailSubject = MessageTemplate.Registration_OTP_Mail_Subject;
                request.MailBody = MessageTemplate.Registration_OTP_Mail_Body;
                var result = await _authenticationService.GenerateAndSendOtpAsync(request);
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
        public async Task<IActionResult> VerifyAccountAsync(OtpViewModel request)
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
    }
}
