using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IShoppyCarRepository:IRepositoryBase<ShoppyCar>
    {
        IEnumerable<ShoppyCar> GetAllShoppyCar();
        ShoppyCar GetShoppyCarById(Guid shoppyCarId);
        void CreateShoppyCar(ShoppyCar shoppyCar);
        void UpdateShoppyCar(ShoppyCar shoppyCar);
        void DeleteShoppyCar(ShoppyCar shoppyCar);
        ShoppyCar GetShoppyCarWithDetails(Guid shoppyCarId);
    }
}
