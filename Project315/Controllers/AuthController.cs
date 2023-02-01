using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project315.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IConfiguration _configuration;
        public AuthController(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel _user)
        {
            var user = _repository.User.GetUserByName(_user.Usuario);
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            else if(_user.Password == _user.Password)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Usuario),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                var token = GetToken(authClaims);
                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
            }
            
            return Unauthorized();
        }
        private JwtSecurityToken GetToken(List<Claim> authclaims)
        {
            var authSign = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authSign,SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
