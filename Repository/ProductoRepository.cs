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

        public IEnumerable<Producto> GetAllProducto()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        public Producto GetProductoById(Guid productoId)
        {
            return FindByCondition(producto => producto.Id.Equals(productoId))
                    .FirstOrDefault();
        }

        public IEnumerable<Producto> ProductosByCategoria(Guid categoriaId)
        {
            return FindByCondition(a => a.CategoriaId.Equals(categoriaId)).ToList();
        }

        public void UpdateProducto(Producto producto)
        {
            Update(producto);
        }
    }
}
