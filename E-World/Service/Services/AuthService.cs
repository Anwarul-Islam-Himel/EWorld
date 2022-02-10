using Domian.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.ResponseModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IAuthService
    {
        Task<Result<object>> Login(string email, string password);
        Task<Result<object>> AddUser(string email, string password, string name);
        Task<string> GenerateToken(string email);
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
        public async Task<string> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<Result<object>> Login(string email, string password) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            Console.WriteLine(user);
            if(user!=null && await _userManager.CheckPasswordAsync(user, password))
            {
                return new Result<object> { StatusCode = 200, Response = new { Token = GenerateToken(email) } };
            }
            return new Result<object> { StatusCode = 404, Response = new { Message = "Username or Password doesn't match" } };
        }

        public async Task<Result<object>> AddUser(string email, string password, string name)
        {
            try
            {
                var isUserExist = await _userManager.FindByEmailAsync(email);
                if (isUserExist != null)
                {
                    return new Result<object> { StatusCode = 400, Response = new { Message = "Account already exists with this email!" } };
                }

                ApplicationUser user = new()
                {
                    UserName = name,
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return new Result<object> { StatusCode = 500, Response = new { Message = "Operation failed, please try again" } };
                }
                return new Result<object> { StatusCode = 200, Response = new { Token = GenerateToken(email) } };
            }
            catch
            {
                return new Result<object> { StatusCode = 500, Response = "Something wen't in the server" };
            }

        }

    }
}
