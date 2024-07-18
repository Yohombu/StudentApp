using JWT_Token_Example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if (user.Username == "admin" && user.Password == "1234")
            {
                _user = new Users { Username = user.Username };
            }
            return _user;
        }

        private string GenerateToken(Users users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
        public IActionResult Login([FromBody] Users user)
        {
            IActionResult response = Unauthorized();

            var authenticatedUser = AuthenticateUser(user);

            if (authenticatedUser != null)
            {
                var tokenString = GenerateToken(authenticatedUser);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

    }
}
