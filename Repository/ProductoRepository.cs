using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ProductoRepository : RepositoryBase<Producto> , IProductoRepository
    {
        public ProductoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Producto> CreateProducto(Producto producto)
        {
            producto = await Create(producto);
            return await Task.FromResult(producto);
        }

        public async Task<bool> DeleteProducto(Producto producto)
        {
            var deleted = await Delete(producto);
            return await Task.FromResult(deleted);
        }

        public async Task<IEnumerable<Producto>> GetAllProducto()
        {
            var producto = await FindAll();
            return await Task.FromResult(producto
                .OrderBy(ow => ow.Name));
        }

        public async Task<Producto> GetProductoById(Guid productoId)
        {
            var producto = await FindByCondition(producto => producto.Id.Equals(productoId));
            return await Task.FromResult(producto.FirstOrDefault());
        }
        public async Task<Producto> UpdateProducto(Producto producto)
        {
            producto = await Update(producto);
            return await Task.FromResult(producto);
        }
    }
}
