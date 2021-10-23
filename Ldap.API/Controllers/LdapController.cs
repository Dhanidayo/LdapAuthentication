using Ldap.BusinessLogic.Interfaces;
using Ldap.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ldap.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LdapController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        public LdapController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("AuthenticateUser")]
        public ActionResult AuthenticateUser([FromBody] UserRequestDTO userRequestDTO)
        {
            try
            {
                var result = _authentication.IsAuthenticated(userRequestDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
                //Console.WriteLine(e.Message);
                //return false;
            }
        }
    }
}
