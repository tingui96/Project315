using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project315.BasicResponses;

namespace Project315.Controllers
{
    [Route("api/producto")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ProductoController(ILoggerManager logger,IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducto()
        {
            try
            {
                var productos = await _repository.Producto.GetAllProducto();
                _logger.LogInfo($"Returned all productos from database.");
                var productosResult = _mapper.Map<IEnumerable<ProductoDTO>>(productos);
                return Ok(new ApiOkResponse(productosResult));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}",Name = "ProductoById")]
        public async Task<IActionResult> GetProductoById(Guid id)
        {
            try
            {
                var producto = await _repository.Producto.GetProductoById(id);

                if (producto is null)
                {
                    _logger.LogError($"producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned producto with id: {id}");

                    var productoResult = _mapper.Map<ProductoDTO>(producto);
                    return Ok(new ApiOkResponse(productoResult));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetproductoById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost, Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateProducto([FromBody] ProductoForCreationDTO producto)
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

                await _repository.Producto.CreateProducto(productoEntity);
                await _repository.Save();

                var createdproducto = _mapper.Map<ProductoDTO>(productoEntity);

                return CreatedAtRoute("ProductoById", new { id = createdproducto.Id }, createdproducto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Createproducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}"), Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateProducto(Guid id, [FromBody] ProductoForUpdateDTO producto)
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

                var productoEntity = await _repository.Producto.GetProductoById(id);
                if (productoEntity is null)
                {
                    _logger.LogError($"Producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(producto, productoEntity);

                var result = await _repository.Producto.UpdateProducto(productoEntity);
                await _repository.Save();

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}"), Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            try
            {
                var producto = await _repository.Producto.GetProductoById(id);
                if (producto == null)
                {
                    _logger.LogError($"Producto with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var result = await _repository.Producto.DeleteProducto(producto);
                await _repository.Save();

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProducto action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
