using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public ProductoController(ILoggerManager logger,IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProducto()
        {
            try
            {
                var productos = _repository.Producto.GetAllProducto();
                _logger.LogInfo($"Returned all productos from database.");
                var productosResult = _mapper.Map<IEnumerable<ProductoDTO>>(productos);
                return Ok(productosResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}",Name = "ProductoById")]
        public IActionResult GetProductoById(Guid id)
        {
            try
            {
                var producto = _repository.Producto.GetProductoById(id);

                if (producto is null)
                {
                    _logger.LogError($"producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned producto with id: {id}");

                    var productoResult = _mapper.Map<ProductoDTO>(producto);
                    return Ok(productoResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetproductoById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateProducto([FromBody] ProductoForCreationDTO producto)
        {
            try
            {
                if (producto is null)
                {
                    _logger.LogError("producto object sent from client is null.");
                    return BadRequest("producto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid producto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var productoEntity = _mapper.Map<Producto>(producto);

                _repository.Producto.CreateProducto(productoEntity);
                _repository.Save();

                var createdproducto = _mapper.Map<ProductoDTO>(productoEntity);

                return CreatedAtRoute("ProductoById", new { id = createdproducto.Id }, createdproducto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Createproducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(Guid id, [FromBody] ProductoForUpdateDTO producto)
        {
            try
            {
                if (producto is null)
                {
                    _logger.LogError("Producto object sent from client is null.");
                    return BadRequest("Producto object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid producto object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var productoEntity = _repository.Producto.GetProductoById(id);
                if (productoEntity is null)
                {
                    _logger.LogError($"Producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(producto, productoEntity);

                _repository.Producto.UpdateProducto(productoEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(Guid id)
        {
            try
            {
                var producto = _repository.Producto.GetProductoById(id);
                if (producto == null)
                {
                    _logger.LogError($"Producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Producto.DeleteProducto(producto);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
