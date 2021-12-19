using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;
using TestEleksApplication.Interfaces;
using TestEleksApplication.Services.AuthenticationService;

namespace TestEleksApplication.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        public async Task<ActionResult<User>> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _authService.Authenticate(model.Login, model.Password);

            if (user == null) return BadRequest(new { message = "Email or password is incorrect" });

            HttpContext.Response.Cookies.Append("Token", user.Token, new CookieOptions { Secure = true, HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok(user);
        }
    }
}
