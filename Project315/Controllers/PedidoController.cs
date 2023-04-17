using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project315.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public PedidoController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPedidosByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;
            if(userId is not null)
            {
                var pedidos = await _repository.Pedido.GetPedidosByUser(userId);
                var pedidosResult = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
                return Ok(pedidosResult);
            }
            return BadRequest();
        }
        [HttpGet("Admin"),Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAllPedido()
        {
            try
            {
                var pedidos = await _repository.Pedido.GetAllPedido();
                _logger.LogInfo($"Returned all pedido from database.");
                var pedidosResult = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
                return Ok(pedidosResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPedidos action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}", Name = "PedidoById")]
        public async Task<IActionResult> GetPedidoById(Guid id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;

                var pedido = await _repository.Pedido.GetPedidoById(id);

                if (pedido is null)
                {
                    _logger.LogError($"pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else if (userId is not null && !await _repository.Pedido.IsMyPedido(id, userId))
                {
                    _logger.LogError($"No puedes modificar este pedido");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned pedido with id: {id}");

                    var pedidoResult = _mapper.Map<PedidoDTO>(pedido);
                    return Ok(pedidoResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetpedidoById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] PedidoForCreationDTO pedido)
        {
            try
            {
                if (pedido is null)
                {
                    _logger.LogError("pedido object sent from client is null.");
                    return BadRequest("pedido object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid pedido object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var producto = await _repository.Producto.GetProductoById(pedido.productoId);
                if (producto is null)
                {
                    _logger.LogError("product object sent from client is null.");
                    return BadRequest("product object is null");
                }
                pedido.producto = producto;
                var pedidoEntity = _mapper.Map<Pedido>(pedido);

                await _repository.Pedido.CreatePedido(pedidoEntity);
                await _repository.Save();

                var createdpedido = _mapper.Map<PedidoDTO>(pedidoEntity);

                return CreatedAtRoute("PedidoById", new { id = createdpedido.Id }, createdpedido);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Createpedido action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(Guid id, [FromBody] PedidoForUpdateDTO pedido)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = identity?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;
                if (pedido is null)
                {
                    _logger.LogError("Pedido object sent from client is null.");
                    return BadRequest("Pedido object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid pedido object sent from client.");
                    return BadRequest("Invalid model object");
                }
                else if (userId is not null && await _repository.Pedido.IsMyPedido(id, userId))
                {
                    _logger.LogError($"No puedes modificar este pedido");
                    return NotFound();
                }
                var pedidoEntity = await _repository.Pedido.GetPedidoById(id);
                if (pedidoEntity is null)
                {
                    _logger.LogError($"Pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(pedido, pedidoEntity);

                await _repository.Pedido.UpdatePedido(pedidoEntity);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePedido action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;
            try
            {
                var pedido = await _repository.Pedido.GetPedidoById(id);
                if (pedido == null)
                {
                    _logger.LogError($"Pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else if (userId is not null && await _repository.Pedido.IsMyPedido(id,userId))
                {
                    _logger.LogError($"No puedes modificar este pedido");
                    return NotFound();
                }
                await _repository.Pedido.DeletePedido(pedido);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePedido action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
