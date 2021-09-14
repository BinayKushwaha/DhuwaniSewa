using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhuwaniSewa.Web.Api.Controller.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        [Route("userprofile")]
        public async Task<IActionResult> UserProfile(int userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid inputs");
                var result = await _userService.GetProfileAsync(userId);
                return Ok(ResponseModel.Success("User profile data reterived successfully.", result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administrator"));
            }
        }
    }
}
