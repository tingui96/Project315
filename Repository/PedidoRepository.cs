using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PedidoRepository:RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void CreatePedido(Pedido pedido)
        {
            Create(pedido);
        }

        public void UpdatePedido(Pedido pedido)
        {
            Update(pedido);
        }

        public async Task<IEnumerable<Pedido>> GetAllPedido()
        {
            var pedido = await FindAll();
            return pedido
                .ToList();
        }
        public async Task<Pedido> GetPedidoById(Guid pedidoId)
        {
            var pedido = await FindByCondition(pedido => pedido.Id.Equals(pedidoId));
            return pedido
                    .FirstOrDefault();
        }

        public void DeletePedido(Pedido pedido)
        {
            Delete(pedido);
        }
        public async Task<Pedido> GetPedidoWithDetails(Guid pedidoId)
        {
            var pedido = await FindByCondition(pedido => pedido.Id.Equals(pedidoId));
            return pedido
                .Include(ac => ac.Producto)
                .FirstOrDefault();
        }
    }
}
