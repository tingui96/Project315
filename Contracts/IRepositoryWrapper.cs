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
        Task Save();
    }
}
