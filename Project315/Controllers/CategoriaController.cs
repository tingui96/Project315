using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CategoriaController(ILoggerManager logger,IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoria()
        {
            try
            {
                var categorias = await _repository.Categoria.GetAllCategoria();
                _logger.LogInfo($"Returned all category from database.");
                var categoriasResult = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
                return Ok(categoriasResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}",Name = "CategoriaById")]
        public async Task<IActionResult> GetCategoriaById(Guid id)
        {
            try
            {
                var categoria = await _repository.Categoria.GetCategoriaById(id);

                if (categoria is null)
                {
                    _logger.LogError($"categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned categoria with id: {id}");

                    var categoriaResult = _mapper.Map<CategoriaDTO>(categoria);
                    return Ok(categoriaResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetcategoriaById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost,Authorize(Roles = "Administrador")]
        public IActionResult CreateCategoria([FromBody] CategoriaForCreationDTO categoria)
        {
            try
            {
                if (categoria is null)
                {
                    _logger.LogError("categoria object sent from client is null.");
                    return BadRequest("categoria object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid categoria object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoriaEntity = _mapper.Map<Categoria>(categoria);

                _repository.Categoria.CreateCategoria(categoriaEntity);
                _repository.Save();

                var createdcategoria = _mapper.Map<CategoriaDTO>(categoriaEntity);

                return CreatedAtRoute("CategoriaById", new { id = createdcategoria.Id }, createdcategoria);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Createcategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}"),Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateCategoria(Guid id, [FromBody] CategoriaForUpdateDTO categoria)
        {
            try
            {
                if (categoria is null)
                {
                    _logger.LogError("Categoria object sent from client is null.");
                    return BadRequest("Categoria object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid categoria object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoriaEntity = await _repository.Categoria.GetCategoriaById(id);
                if (categoriaEntity is null)
                {
                    _logger.LogError($"Categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(categoria, categoriaEntity);

                await _repository.Categoria.UpdateCategoria(categoriaEntity);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}"), Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            try
            {
                var categoria = await _repository.Categoria.GetCategoriaById(id);
                if (categoria == null)
                {
                    _logger.LogError($"Categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _repository.Categoria.DeleteCategoria(categoria);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/producto")]
        public async Task<IActionResult> GetCategoriaWithDetails(Guid id)
        {
            try
            {
                var categoria = await _repository.Categoria.GetCategoriaWithDetails(id);

                if (categoria == null)
                {
                    _logger.LogError($"Categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned categoria with details for id: {id}");

                    var categoriaResult = _mapper.Map<CategoriaWithProductDTO>(categoria);
                    return Ok(categoriaResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCategoriaWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
