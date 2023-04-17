using Entities.Models;

namespace Contracts
{
    public interface IShoppyCarRepository:IRepositoryBase<ShoppyCar>
    {
        Task<IEnumerable<ShoppyCar>> GetAllShoppyCar();
        Task<ShoppyCar> GetShoppyCarById(Guid shoppyCarId);
        Task<ShoppyCar> CreateShoppyCar(ShoppyCar shoppyCar);
        Task<ShoppyCar> UpdateShoppyCar(ShoppyCar shoppyCar);
        Task<bool> DeleteShoppyCar(ShoppyCar shoppyCar);
        Task<IEnumerable<ShoppyCar>> GetShoppyCarsByUser(string userId);
        Task<bool> IsMyShoppyCar(Guid id,string userId);

        
    }
}
