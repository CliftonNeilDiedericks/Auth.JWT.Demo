using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.JWT.Demo.Middleware;
using Auth.JWT.Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.JWT.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jwtAuthenticationManager;
        public NameController(IJWTAuthenticationManager jwtAuthenticationManager )
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/Name
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Cape Town", "Gqeberha","Johannesburg","Durban" };
        }

        // GET: api/Name/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
           var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            if(token==null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
