using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ldap.BusinessLogic.Interfaces;
using Ldap.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ldap.BusinessLogic.Implementations
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJSONWebToken(User user)    
        {    
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));        
            
            var authClaims = new List<Claim>
            {    
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
            };
            
            var token = new JwtSecurityToken
                (audience: _configuration["JWTSettings:Audience"],
                issuer: _configuration["JWTSettings:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );    
            
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }  
    }
}

