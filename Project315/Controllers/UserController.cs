using AutoMapper;
using Contracts;
using Entities.Auth;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project315.Helpers;

namespace Project315.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UserController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthRepository _authRepository;

        public UserController(IRepositoryWrapper repository, IAuthRepository authRepository)
        {
            _repository = repository;
            _authRepository = authRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.User.FindAll();
            return Ok(users);
        }
        [HttpGet("{rol}/rol", Name ="GetUsersByRol")]
        public async Task<IActionResult> GetUsers(string rol)
        {
            IQueryable<User> query;
            if (rol != null)
            {
                var role = await _repository.Role.FindByCondition(r => r.Name.Equals(rol));
                if (role != null)
                {
                    query = await _repository.User.FindAllInRole(role.Id);
                }
                else
                    query = _repository.User.UsersQueryable();
            }
            else
            {
                return BadRequest("Rol no puede ser null");
            }            
            return Ok(query.ToList());
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _repository.User.UsersQueryable().Where(u => u.Id.Equals(id.ToString())).FirstAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _repository.User.GetById(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var shoppycar = await _repository.ShoppyCar.FindByCondition(v => v.UserId.Equals(user.Id));
            foreach (var sho in shoppycar)
                _repository.ShoppyCar.Delete(sho);

            _repository.User.Delete(user);
            _repository.Save();

            return Ok(user);
        }
        [HttpPost("authorize/{id}")]
        public async Task<IActionResult> AuthorizeUser(string id)
        {
            var user = await _repository.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.activo = true;

            _repository.User.Update(user);
            _repository.Save();

            return Ok(user);
        }

        [HttpPost("unauthorize/{id}")]
        public async Task<IActionResult> UnAuthorizeUser(string id)
        {
            var user = await _repository.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            user.activo = false;
            _repository.User.Update(user);
            _repository.Save();
            return Ok(user);
        }
    }
}
