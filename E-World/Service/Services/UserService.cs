using Domian.Models;
using Microsoft.AspNetCore.Identity;
using Service.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IUserService
    {
        Task<Result<object>> AddUser(string email, string password,string name);
    }
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                return new Result<object> { StatusCode = 200, Response = user };
            }
            catch
            {
                return new Result<object> { StatusCode = 500, Response = "Something wen't in the server" };
            }
            
        }
    }
}
