using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, Authorize(Roles = "Administrador")]
    public class RolesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public RolesController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _repoWrapper.Role.FindAll();
            return Ok(roles);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRol(string id)
        {
            var roles = await _repoWrapper.Role.GetById(id);
            await _repoWrapper.Role.Delete(roles);
            await _repoWrapper.Save();
            return Ok();
        }
    }
}
