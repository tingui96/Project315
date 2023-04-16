using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PedidoRepository:RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public async Task<Pedido> CreatePedido(Pedido pedido)
        {
            pedido = await Create(pedido);
            return await Task.FromResult(pedido);
        }

        public async Task<Pedido> UpdatePedido(Pedido pedido)
        {
            pedido = await Update(pedido);
            return await Task.FromResult(pedido);

        }

        public async Task<IEnumerable<Pedido>> GetAllPedido()
        {
            var pedido = await FindAll();
            return await Task.FromResult(pedido);                
        }
        public async Task<Pedido> GetPedidoById(Guid pedidoId)
        {
            var pedido = await FindByCondition(pedido => pedido.Id.Equals(pedidoId));
            var pedidoById = pedido.Include(ac => ac.Producto).FirstOrDefault();
            return await Task.FromResult(pedidoById);
        }

        public async Task<bool> DeletePedido(Pedido pedido)
        {
            var deleted = await Delete(pedido);
            return await Task.FromResult(deleted);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByUser(string userId)
        {
            var pedidos = await FindByCondition(pedido => pedido.ShoppyCar != null && pedido.ShoppyCar.UserId.Equals(userId));
            return await Task.FromResult(pedidos.Include(ac => ac.Producto));
        }

        public async Task<bool> IsMyPedido(Guid id, string userId)
        {
            var pedido = await FindByCondition(pedido => pedido.Id.Equals(id));
            var pedidoIdentity = pedido.Include(ac => ac.ShoppyCar).FirstOrDefault();
            var identity = pedidoIdentity?.ShoppyCar?.UserId;
            if (identity != null && identity.Equals(userId))
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
    }
}
