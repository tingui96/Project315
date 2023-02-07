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
                .Include(ac => ac.Producto)
                    .FirstOrDefault();
        }

        public void DeletePedido(Pedido pedido)
        {
            Delete(pedido);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByUser(string userId)
        {
            var pedidos = await FindByCondition(pedido=>pedido.ShoppyCar.UserId.Equals(userId));
            return pedidos.Include(ac => ac.Producto).ToList();
        }

        public async Task<bool> IsMyPedido(Guid id, string userId)
        {
            var pedido = await FindByCondition(pedido => pedido.Id.Equals(id));
            var pedidoIdentity = pedido.Include(ac => ac.ShoppyCar).FirstOrDefault();
            var identity = pedidoIdentity.ShoppyCar.UserId;
            if (identity != null && identity.Equals(userId))
                return true;
            return false;
        }
    }
}
