using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.JwtService;
namespace timestamp_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public AuthController(JwtService jwtService) {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([ FromBody] LoginModel model) {
            if (model.Username == "admin" && model.Password == "password") {
                var token = _jwtService.GenerrateToken(model.Username);
                return Ok( new { token });
            }
            return Unauthorized();
        }

        [HttpGet("secret")]
        [Authorize]
        public IActionResult SecretData()
        {
            return Ok("This is protected data.");
        }
    }


    public class LoginModel {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
