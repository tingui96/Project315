using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> GetAllPedido();
        Task<Pedido> GetPedidoById(Guid pedidoId);
        Task<Pedido> CreatePedido(Pedido pedido);
        Task<Pedido> UpdatePedido(Pedido pedido);
        Task<bool> DeletePedido(Pedido pedido);
        Task<IEnumerable<Pedido>> GetPedidosByUser(string userId);
        Task<bool> IsMyPedido(Guid id, string userId);
    }
}
