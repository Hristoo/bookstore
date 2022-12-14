using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models.Users;
using Confluent.Kafka;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private readonly IOptions<MyJsonSettings> _myJsonSettings;
        private readonly Producer<int, int> _producer;

        public IdentityController(IIdentityService identityService, IConfiguration configuration, IOptions<MyJsonSettings> myJsonSettings, Producer<int, int> producer)
        {
            _identityService = identityService;
            _configuration = configuration;
            _myJsonSettings = myJsonSettings;
            _producer = producer;
        }

        [AllowAnonymous]
        [HttpPost(nameof(SendMessage))]
        public async Task<ActionResult> SendMessage(int key, int value)
        {
            var msg = new Message<int, int>()
            {
                Key = key,
                Value = value
            };

            await _producer.SendMessage(msg);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest($"Username or password missing");
            }

            var result = await _identityService.CreateAsync(user);
            return result.Succeeded ? Ok(result) : BadRequest(result);

        } 

        [AllowAnonymous]
        [HttpPost(nameof(Post))]
        public async Task<IActionResult> Post(LoginRequest loginRequest)
        {
            if (loginRequest != null && !string.IsNullOrEmpty(loginRequest.UserName) && !string.IsNullOrEmpty(loginRequest.Password))
            {
                var user = await _identityService.CheckUserAndPass(loginRequest.UserName, loginRequest.Password);

                if (user != null)
                {
                    var userRoles = await _identityService.GetUserRoles(user);

                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserId", user.UserName.ToString()),
                        new Claim("DispayName", user.DisplayName ?? String.Empty),
                        new Claim("UserName", user.UserName ?? String.Empty),
                        new Claim("Email", user.Email ?? String.Empty),
                        new Claim("User", "User")
                    };

                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

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


        [AllowAnonymous]
        [HttpPost(nameof(GetUserIdAsync))]
        public async Task<IActionResult> GetUserIdAsync([FromBody] UserInfo user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest($"User missing");
            }

            var result = await _identityService.GetUserIdAsync(user);
            return result != null ? Ok(result) : BadRequest(result);

        }
    }
}
