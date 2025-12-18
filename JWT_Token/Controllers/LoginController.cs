using JWT_Token.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JWT_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            return "User not found";
        }

        private string GenerateToken(UserModel model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserName),
                new Claim(ClaimTypes.Role, model.Role),
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var existinguser = UserConstants.users.FirstOrDefault(u => u.UserName.ToLower() == userLogin.UserName.ToLower() && u.Password == userLogin.Password);

            if(existinguser != null)
            {
                return existinguser;
            }
            return null;
        }
    }
}
