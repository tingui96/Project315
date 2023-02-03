using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project315.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;
        public PedidoController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
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
                var pedido = await _repository.Pedido.GetPedidoById(id);

                if (pedido is null)
                {
                    _logger.LogError($"pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned pedido with id: {id}");

                    var pedidoResult = _mapper.Map<CategoriaDTO>(pedido);
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
        public IActionResult CreatePedido([FromBody] PedidoForCreationDTO pedido)
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

                var pedidoEntity = _mapper.Map<Pedido>(pedido);

                _repository.Pedido.CreatePedido(pedidoEntity);
                _repository.Save();

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

                var pedidoEntity = await _repository.Pedido.GetPedidoById(id);
                if (pedidoEntity is null)
                {
                    _logger.LogError($"Pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(pedido, pedidoEntity);

                _repository.Pedido.UpdatePedido(pedidoEntity);
                _repository.Save();

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
            try
            {
                var pedido = await _repository.Pedido.GetPedidoById(id);
                if (pedido == null)
                {
                    _logger.LogError($"Pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Pedido.DeletePedido(pedido);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePedido action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/producto")]
        public async Task<IActionResult> GetPedidoWithDetails(Guid id)
        {
            try
            {
                var pedido = await _repository.Pedido.GetPedidoWithDetails(id);

                if (pedido == null)
                {
                    _logger.LogError($"Pedido with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned pedido with details for id: {id}");

                    var pedidoResult = _mapper.Map<PedidoWithProductDTO>(pedido);
                    return Ok(pedidoResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPedidoWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
