using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project315.Controllers
{
    [Route("api/shoppycar")]
    [ApiController]
    [Authorize]
    public class ShoppyCarController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public ShoppyCarController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllShoppyCarByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
            var shoppyCars = await _repository.ShoppyCar.GetShoppyCarsByUser(userId);
            var shoppyCarsResult = _mapper.Map<IEnumerable<ShoppyCarDTO>>(shoppyCars);
            return Ok(shoppyCarsResult);
        }
        [HttpGet("Admin"),Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAllShoppyCar()
        {
            try
            {
                var shoppyCars = await _repository.ShoppyCar.GetAllShoppyCar();
                _logger.LogInfo($"Returned all shoppycar from database.");
                var shoppyCarsResult = _mapper.Map<IEnumerable<ShoppyCarDTO>>(shoppyCars);
                return Ok(shoppyCarsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllShoppyCar action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}", Name = "ShoppyCarById")]
        public async Task<IActionResult> GetShoppyCarById(Guid id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                var shoppyCar = await _repository.ShoppyCar.GetShoppyCarById(id);

                if (shoppyCar is null)
                {
                    _logger.LogError($"shoppyCar with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else if (await _repository.ShoppyCar.IsMyShoppyCar(id, userId))
                {
                    _logger.LogError($"No puedes modificar este ShoppyCar");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned shoppyCar with id: {id}");

                    var shoppyCarResult = _mapper.Map<ShoppyCarDTO>(shoppyCar);
                    return Ok(shoppyCarResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShoppyCarById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateShoppyCar()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                var shoppycar = new ShoppyCarForCreationDTO();
                shoppycar.userId = userId;

                var shoppyCarEntity = _mapper.Map<ShoppyCar>(shoppycar);

                _repository.ShoppyCar.CreateShoppyCar(shoppyCarEntity);
                _repository.Save();

                var createdshoppyCar = _mapper.Map<ShoppyCarDTO>(shoppyCarEntity);

                return CreatedAtRoute("ShoppyCarById", new { id = createdshoppyCar.Id }, createdshoppyCar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateShoppyCar action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppyCar(Guid id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                var shoppyCar = await _repository.ShoppyCar.GetShoppyCarById(id);
                if (shoppyCar == null)
                {
                    _logger.LogError($"ShoppyCar with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else if (await _repository.ShoppyCar.IsMyShoppyCar(id, userId))
                {
                    _logger.LogError($"No puedes modificar este ShoppyCar");
                    return NotFound();
                }
                _repository.ShoppyCar.DeleteShoppyCar(shoppyCar);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteShoppyCar action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/pedidos")]
        public async Task<IActionResult> GetShoppyCarWithDetails(Guid id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                var shoppyCar = await _repository.ShoppyCar.GetShoppyCarWithDetails(id);

                if (shoppyCar == null)
                {
                    _logger.LogError($"ShoppyCar with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else if (await _repository.ShoppyCar.IsMyShoppyCar(id, userId))
                {
                    _logger.LogError($"No puedes modificar este ShoppyCar");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned shoppyCar with details for id: {id}");

                    var shoppyCarResult = _mapper.Map<ShoppyCarWithDetailDTO>(shoppyCar);
                    return Ok(shoppyCarResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShoppyCarWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppyCar(Guid id, [FromBody] ShoppyCarForUpdateDTO shoppyCar)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                if (shoppyCar is null)
                {
                    _logger.LogError("ShoppyCar object sent from client is null.");
                    return BadRequest("ShoppyCar object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid shoppyCar object sent from client.");
                    return BadRequest("Invalid model object");
                }
                else if (await _repository.ShoppyCar.IsMyShoppyCar(id, userId))
                {
                    _logger.LogError($"No puedes modificar este ShoppyCar");
                    return NotFound();
                }
                var shoppyCarEntity = await _repository.ShoppyCar.GetShoppyCarById(id);
                if (shoppyCarEntity is null)
                {
                    _logger.LogError($"ShoppyCar with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(shoppyCar, shoppyCarEntity);

                _repository.ShoppyCar.UpdateShoppyCar(shoppyCarEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateShoppyCar action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
