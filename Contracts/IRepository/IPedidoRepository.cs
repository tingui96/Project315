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
        IEnumerable<Pedido> GetAllPedido();
        Pedido GetPedidoById(Guid pedidoId);
        void CreatePedido(Pedido pedido);
        void UpdatePedido(Pedido pedido);
        void DeletePedido(Pedido pedido);
        Pedido GetPedidoWithDetails(Guid pedidoId);
    }
}
