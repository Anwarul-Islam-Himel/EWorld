using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Threading.Tasks;
using E_World.RequestModel;

namespace E_World.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("add-user")]
        public async Task<IActionResult> AddUser([FromBody] RegisterModel model)
        {
            var response = await _authService.AddUser(model.Email, model.Password, model.Name);
            return StatusCode(response.StatusCode, response.Response);
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model.Email, model.Password);
            return StatusCode(response.StatusCode, response.Response);
        }
    }
}
