using Entities.Models;

namespace Contracts
{
    public interface IProductoRepository : IRepositoryBase<Producto>
    {
        Task<IEnumerable<Producto>> GetAllProducto();
        Task<Producto> GetProductoById(Guid productoId);
        Task<Producto> CreateProducto(Producto producto);
        Task<Producto> UpdateProducto(Producto producto);
        Task<bool> DeleteProducto(Producto producto);
    }
}
