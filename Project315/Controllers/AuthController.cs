using Entities.Auth;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project315.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        [HttpPost("register/administrador"),Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegisterAdministrador([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.Register(model);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.Register(model);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _repository.Login(model);
            if (user == null)
                return BadRequest("Usuario o contraseña invalidos");


            return await BuildToken(user);

        }

        private async Task<IActionResult> BuildToken(User user)
        {
            var userRoles = await _repository.GetAllRoles(user);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("id",user.Id),
            };
            foreach (var rol in userRoles)
            {
                claim.Add(new Claim(ClaimTypes.Role, rol));
            }
            var token = GetToken(claim);
            return Ok(
                new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            } );
        }
        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500, "Internal Server Error");
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        private JwtSecurityToken GetToken(List<Claim> authclaims)
        {
            var key = _configuration.GetConnectionString("Jwt:Key") ?? string.Empty;
            var authSign = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authSign,SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
