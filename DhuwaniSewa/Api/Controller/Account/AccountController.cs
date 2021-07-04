using DhuwaniSewa.Application.Auth;
using DhuwaniSewa.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils.CustomException;

namespace DhuwaniSewa.Web.Api.Controller.Account
{
    [Route("api/account")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IAuthenticationApplication _authenticationApplication;
        public AccountController(IUserApplication userApplication,
            IAuthenticationApplication authenticationApplication) 
        {
            this._userApplication = userApplication;
            this._authenticationApplication = authenticationApplication;
        }

        [HttpPost]
        [Route("register")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
               var result= await _userApplication.Register(request);
                return Ok(ResponseModel.Success("User registered successfully.",result));
            }
            catch(CustomException ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ResponseModel.Info(ex.Message, request));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs");
                var result = await _authenticationApplication.Login(request);
                return Ok(ResponseModel.Success("Login successfull.",result));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
                var result =await _authenticationApplication.GetRefreshedToken(request);
                return Ok(ResponseModel.Success("Tokens are refreshed successfully.", result));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }
    }
}
