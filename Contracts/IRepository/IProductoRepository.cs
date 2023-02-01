using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IProductoRepository : IRepositoryBase<Producto>
    {
        IEnumerable<Producto> GetAllProducto();
        IEnumerable<Producto> ProductosByCategoria(Guid categoriaId);
        Producto GetProductoById(Guid productoId);
        void CreateProducto(Producto producto);
        void UpdateProducto(Producto producto);
        void DeleteProducto(Producto producto);
    }
}
