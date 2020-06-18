using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DevsController : ControllerBase
    {

        [HttpPost("api/v1/DevModel")]
        public IActionResult Create([FromBody] DevModel dev)
        {
            var valid = ModelState.IsValid;

            return Ok();
        }
    }
}
