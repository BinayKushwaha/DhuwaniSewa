using DhuwaniSewa.Domain;
using DhuwaniSewa.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DhuwaniSewa.Web.Api.Controller.Client
{
    [Route("api/serviceProvider")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IServiceProviderService _serviceProviderService;
        public ServiceProviderController(IServiceProviderService serviceProviderService)
        {
            this._serviceProviderService = serviceProviderService;
        }
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Save([FromBody] ServiceProviderViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Inputs");
                int Id = await _serviceProviderService.Save(request);
                return Ok(ResponseModel.Success("Service Provider details save succesfully.", new { Id = Id }));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));

            }
        }
        [HttpPut]
        [Route("put")]
        public IActionResult update([FromBody] ServiceProviderViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Inputs");
                 _serviceProviderService.Update(request);
                return Ok(ResponseModel.Success("Service Provider details save succesfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));

            }
        }
        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                var result =  _serviceProviderService.GetAll();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));
            }
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = await _serviceProviderService.Get(Id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseModel.Error("Something went wrong. Please contact administartor."));
            }
        }
    }
}
