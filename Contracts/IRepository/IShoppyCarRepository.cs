using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IShoppyCarRepository:IRepositoryBase<ShoppyCar>
    {
        Task<IEnumerable<ShoppyCar>> GetAllShoppyCar();
        Task<ShoppyCar> GetShoppyCarById(Guid shoppyCarId);
        void CreateShoppyCar(ShoppyCar shoppyCar);
        void UpdateShoppyCar(ShoppyCar shoppyCar);
        void DeleteShoppyCar(ShoppyCar shoppyCar);
        Task<ShoppyCar> GetShoppyCarWithDetails(Guid shoppyCarId);
        Task<IEnumerable<ShoppyCar>> GetShoppyCarsByUser(string userId);
        Task<bool> IsMyShoppyCar(Guid id,string userId);

        
    }
}
