using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhuwaniSewa.Web.Api.Controller.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSeekerController : ControllerBase
    {
        public IActionResult Index()
        {
            return null;
        }
    }
}
