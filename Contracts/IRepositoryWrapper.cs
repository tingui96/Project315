using Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoriaRepository Categoria { get; }
        IProductoRepository Producto { get; }
        IShoppyCarRepository ShoppyCar { get; }
        IPedidoRepository Pedido { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void Save();
    }
}
