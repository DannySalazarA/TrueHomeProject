using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueHome.WebAPI.Entieties;

namespace TrueHome.WebAPI.Auth
{
    public class AuthenticationJWT
    {
        private IConfiguration _config;
        public AuthenticationJWT(IConfiguration config)
        {
            _config = config;
        }

        private List<User> _users = new List<User>
        {
            new User {Username = "Test", Password = "Test", EmailAddress = "test@test.com" }
        };

        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User AuthenticateUser(User login)
        {

            var user = _users.SingleOrDefault(x => x.Username == login.Username && x.Password == login.Password);


            return user;
        }
    }
}
