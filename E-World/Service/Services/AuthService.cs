using Domian.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.ResponseModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IAuthService
    {
        Task<Result<object>> Loging(string email, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<Result<object>> Loging(string email, string password) 
        {
            var user = await _userManager.FindByIdAsync(email);
            if(user!=null && await _userManager.CheckPasswordAsync(user, password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key:SecretKey"]));
                var token = new JwtSecurityToken(
                   issuer: _configuration["ApiBaseUrl"],
                   expires: DateTime.Now.AddDays(7),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
               );
                return new Result<object> { StatusCode = 200, Response = new { Token = new JwtSecurityTokenHandler().WriteToken(token) } };
            }
            return new Result<object> { StatusCode = 404, Response = new { Message = "Username or Password doesn't match" } };
        }
        
    }
}
