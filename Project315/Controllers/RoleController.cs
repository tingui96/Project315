using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project315.BasicResponses;

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
            return Ok(new ApiOkResponse(roles));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRol(string id)
        {
            var roles = await _repoWrapper.Role.GetById(id);
            var result = await _repoWrapper.Role.Delete(roles);
            await _repoWrapper.Save();
            return Ok(new ApiOkResponse(result));
        }
    }
}
