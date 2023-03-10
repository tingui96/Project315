using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> GetAllPedido();
        Task<Pedido> GetPedidoById(Guid pedidoId);
        void CreatePedido(Pedido pedido);
        void UpdatePedido(Pedido pedido);
        void DeletePedido(Pedido pedido);
        Task<IEnumerable<Pedido>> GetPedidosByUser(string userId);
        Task<bool> IsMyPedido(Guid id, string userId);
    }
}
