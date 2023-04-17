using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private ICategoriaRepository? _categoria;
        private IProductoRepository? _producto;
        private IShoppyCarRepository? _shoppyCar;
        private IPedidoRepository? _pedido;
        private IUserRepository? _user;
        private IRoleRepository? _role;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ICategoriaRepository Categoria
        {
            get
            {
                _categoria ??= new CategoriaRepository(_repoContext);
                return _categoria;
            }
        }

        public IProductoRepository Producto
        {
            get
            {
                _producto ??= new ProductoRepository(_repoContext);
                return _producto;
            }
        }

        public IShoppyCarRepository ShoppyCar
        { 
            get 
            {
                _shoppyCar ??= new ShoppyCarRepository(_repoContext);
                return _shoppyCar;
            }
        }

        public IPedidoRepository Pedido
        {
            get
            {
                _pedido ??= new PedidoRepository(_repoContext);
                return _pedido;
            }
        }

        public IUserRepository User
        {
            get
            {
                _user ??= new UserRepository(_repoContext);
                return _user;
            }
        }

        public IRoleRepository Role 
        {
            get
            {
                _role ??= new RoleRepository(_repoContext);
                return _role;
            }
        }

        public async Task Save()
        {
            _repoContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
