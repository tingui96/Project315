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

        public IEnumerable<Pedido> GetAllPedido()
        {
            return FindAll()
                .ToList();
        }
        public Pedido GetPedidoById(Guid pedidoId)
        {
            return FindByCondition(pedido => pedido.Id.Equals(pedidoId))
                    .FirstOrDefault();
        }

        public void DeletePedido(Pedido pedido)
        {
            Delete(pedido);
        }
        public Pedido GetPedidoWithDetails(Guid pedidoId)
        {
            return FindByCondition(pedido => pedido.Id.Equals(pedidoId))
                .Include(ac => ac.Producto)
                .FirstOrDefault();
        }
    }
}
