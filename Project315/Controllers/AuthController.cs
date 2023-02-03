using AutoMapper;
using Contracts;
using Entities.Auth;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project315.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _repository;
        private IConfiguration _configuration;
        private IMapper _mapper;
        public AuthController(IAuthRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _mapper.Map<User>(model);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _repository.DeleteUser(id.ToString());
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok(result);
        }
        [HttpGet("{rol}")]
        public async Task<IActionResult> GetUserInRole(string rol)
        {
            var result = _repository.GetUsersInRole(rol);
            return Ok(result);
        }

        private async Task<IActionResult> BuildToken(User user)
        {
            var userRoles = await _repository.GetAllRoles(user);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
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
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //var role = await _repository.GetAllRoles(user);
            //
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim(ClaimTypes.Role, role.First())
            //    }),
            //    Expires = DateTime.Now.AddDays(1),
            //    SigningCredentials = signinCredentials
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);
            //return Ok(new { tokenString });
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
            var authSign = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
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
