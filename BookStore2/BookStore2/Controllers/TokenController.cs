using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoService _userInfoService;


        public TokenController(IConfiguration configuration, IEmployeeService employeeService, IUserInfoService userInfoService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
            _userInfoService = userInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInfo userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) && !string.IsNullOrEmpty(userData.Password))
            {
                var user = await _userInfoService.GetUserInfoAsync(userData.Email, userData.Password);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserId", user.UserName.ToString()),
                        new Claim("DispayName", user.DisplayName ?? String.Empty),
                        new Claim("UserName", user.UserName ?? String.Empty),
                        new Claim("Email", user.Email ?? String.Empty)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"], 
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Missing username and/or password");
            }
        }
    }
}
