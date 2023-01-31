using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICategoriaRepository _categoria;
        private IProductoRepository _producto;
        private IShoppyCarRepository _shoppyCar;
        private IPedidoRepository _pedido;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ICategoriaRepository Categoria
        {
            get
            {
                if (_categoria == null)
                    _categoria = new CategoriaRepository(_repoContext);
                return _categoria;
            }
        }

        public IProductoRepository Producto
        {
            get
            {
                if (_producto == null)
                    _producto = new ProductoRepository(_repoContext);
                return _producto;
            }
        }

        public IShoppyCarRepository ShoppyCar
        { 
            get 
            {
                if(_shoppyCar== null)
                    _shoppyCar = new ShoppyCarRepository(_repoContext);
                return _shoppyCar;
            }
        }

        public IPedidoRepository Pedido
        {
            get
            {
                if(_pedido==null)
                    _pedido = new PedidoRepository(_repoContext);
                return _pedido;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
