using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrueHome.WebAPI.Auth;
using TrueHome.WebAPI.Entieties;

namespace TrueHome.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [Route("LoginUser")]
        [HttpPost]
        public IActionResult Login(User login)
        {
            AuthenticationJWT authenticationJWT = new AuthenticationJWT(_config);
            IActionResult response = Unauthorized();
            var user = authenticationJWT.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = authenticationJWT.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
