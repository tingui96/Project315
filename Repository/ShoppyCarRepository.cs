using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShoppyCarRepository : RepositoryBase<ShoppyCar> , IShoppyCarRepository
    {
        public ShoppyCarRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateShoppyCar(ShoppyCar shoppyCar)
        {
            Create(shoppyCar);
        }

        public void UpdateShoppyCar(ShoppyCar shoppyCar)
        {
            Update(shoppyCar);
        }

        public async Task<IEnumerable<ShoppyCar>> GetAllShoppyCar()
        {
            var shoppycar = await FindAll();
            return shoppycar
                .Include(ac=>ac.Pedidos)
                .OrderBy(ow => ow.Created)
                .ToList();
        }
        public async Task<ShoppyCar> GetShoppyCarById(Guid shoppyCarId)
        {
            var shoppy = await FindByCondition(shoppyCar => shoppyCar.Id.Equals(shoppyCarId));
          
            return shoppy.Include(ac => ac.Pedidos)
                .ThenInclude(b => b.Producto)
                    .FirstOrDefault();
        }

        public void DeleteShoppyCar(ShoppyCar shoppyCar)
        {
            Delete(shoppyCar);
        }

        public async Task<IEnumerable<ShoppyCar>> GetShoppyCarsByUser(string userId)
        {
            var shoppycars = await FindByCondition(shoppy => shoppy.UserId.Equals(userId));
            return shoppycars.Include(ac=>ac.Pedidos).ThenInclude(b=>b.Producto).ToList();
        }

        public async Task<bool> IsMyShoppyCar(Guid id, string userId)
        {
            var shoppy = await FindByCondition(shoppyCar => shoppyCar.Id.Equals(id));
            var shoppyIdentity = shoppy.Include(ac => ac.User).FirstOrDefault();
            if(shoppyIdentity.User != null && shoppyIdentity.User.Id.Equals(userId))
                return true;
            return false;
        }
    }
}
