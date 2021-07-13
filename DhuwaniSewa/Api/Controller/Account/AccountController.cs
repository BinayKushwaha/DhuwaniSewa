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
        public async Task<IActionResult> Register(RegisterUserViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs.");
               var result= await _userService.Register(request);
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
                var result = await _authenticationService.Login(request);
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
                var result =await _authenticationService.GetRefreshedToken(request);
                return Ok(ResponseModel.Success("Tokens are refreshed successfully.", result));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }
    }
}
