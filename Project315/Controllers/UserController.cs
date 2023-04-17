using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project315.BasicResponses;

namespace Project315.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.User.FindAll();
            return Ok(new ApiOkResponse(users));
        }
        [HttpGet("{rol}/rol", Name ="GetUsersByRol")]
        public async Task<IActionResult> GetUsers(string rol)
        {
            IQueryable<User> query;
            if (rol != null)
            {
                var role = await _repository.Role.FindByCondition(r => r.Name != null && r.Name.Equals(rol));
                if (role != null)
                {
                    query = await _repository.User.FindAllInRole(role.Id);
                }
                else
                    query = await _repository.User.UsersQueryable();
            }
            else
            {
                return BadRequest("Rol no puede ser null");
            }            
            return Ok(new ApiOkResponse(query.ToList()));
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _repository.User.UsersQueryable();
            var userById = user.Where(u => u.Id.Equals(id.ToString())).FirstAsync();
            if (userById == null)
            {
                return NotFound();
            }
            return Ok(new ApiOkResponse(userById));
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
            {
                await _repository.ShoppyCar.Delete(sho);
            }
            var result = await _repository.User.Delete(user);
            await _repository.Save();
            return Ok(new ApiOkResponse(user));
        }
        [HttpPost("authorize/{id}")]
        public async Task<IActionResult> AuthorizeUser(string id)
        {
            var user = await _repository.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            user.Activo = true;
            var result = await _repository.User.Update(user);
            await _repository.Save();

            return Ok(new ApiOkResponse(result));
        }

        [HttpPost("unauthorize/{id}")]
        public async Task<IActionResult> UnAuthorizeUser(string id)
        {
            var user = await _repository.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            user.Activo = false;
            await _repository.User.Update(user);
            await _repository.Save();
            return Ok(new ApiOkResponse(user));
        }
    }
}
