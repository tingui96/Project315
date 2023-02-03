using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public RolesController(IRepositoryWrapper repoWrapper,IMapper mapper, IConfiguration configuration)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _configuration = configuration;
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
            _repoWrapper.Role.Delete(roles);
            _repoWrapper.Save();
            return Ok();
        }
    }
}
