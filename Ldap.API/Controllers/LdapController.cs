using Ldap.BusinessLogic.Interfaces;
using Ldap.Models.DTOs;
using Ldap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ldap.BusinessLogic.Implementations;

namespace Ldap.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LdapController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly ITokenGenerator _tokenGenerator;
        public LdapController(IAuthentication authentication, ITokenGenerator tokenGenerator)
        {
            _authentication = authentication;
            _tokenGenerator = tokenGenerator;
        }

        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public IActionResult AuthenticateUser(User user)
        {
            try
            {
                var result = _authentication.IsAuthenticated(user);
                if (result != null)
                {
                    var tokenString = _tokenGenerator.GenerateJSONWebToken(user);
                    return Ok(new {Token = tokenString, Message = "Success", result});
                    //return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
                //Console.WriteLine(e.Message);
                //return false;
            }
        }
    }
}
