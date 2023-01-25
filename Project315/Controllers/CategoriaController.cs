using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public CategoriaController(ILoggerManager logger,IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategoria()
        {
            try
            {
                var categorias = _repository.Categoria.GetAllCategoria();
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
        public IActionResult GetCategoriaById(Guid id)
        {
            try
            {
                var categoria = _repository.Categoria.GetCategoriaById(id);

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
        [HttpPost]
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
        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(Guid id, [FromBody] CategoriaForUpdateDTO categoria)
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

                var categoriaEntity = _repository.Categoria.GetCategoriaById(id);
                if (categoriaEntity is null)
                {
                    _logger.LogError($"Categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(categoria, categoriaEntity);

                _repository.Categoria.UpdateCategoria(categoriaEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(Guid id)
        {
            try
            {
                var categoria = _repository.Categoria.GetCategoriaById(id);
                if (categoria == null)
                {
                    _logger.LogError($"Categoria with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Categoria.DeleteCategoria(categoria);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCategoria action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/producto")]
        public IActionResult GetCategoriaWithDetails(Guid id)
        {
            try
            {
                var categoria = _repository.Categoria.GetCategoriaWithDetails(id);

                if (categoria == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
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
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
