using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductoRepository : RepositoryBase<Producto> , IProductoRepository
    {
        public ProductoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateProducto(Producto producto)
        {
            Create(producto);
        }

        public void DeleteProducto(Producto producto)
        {
            Delete(producto);
        }

        public async Task<IEnumerable<Producto>> GetAllProducto()
        {
            var producto = await FindAll();
            return producto
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        public async Task<Producto> GetProductoById(Guid productoId)
        {
            var producto = await FindByCondition(producto => producto.Id.Equals(productoId));
            return producto
                    .FirstOrDefault();
        }
        public void UpdateProducto(Producto producto)
        {
            Update(producto);
        }
    }
}
