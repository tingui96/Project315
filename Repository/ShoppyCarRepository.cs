using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ShoppyCarRepository : RepositoryBase<ShoppyCar> , IShoppyCarRepository
    {
        public ShoppyCarRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<ShoppyCar> CreateShoppyCar(ShoppyCar shoppyCar)
        {
            shoppyCar = await Create(shoppyCar);
            return await Task.FromResult(shoppyCar);
        }

        public async Task<ShoppyCar> UpdateShoppyCar(ShoppyCar shoppyCar)
        {
            shoppyCar = await Update(shoppyCar);
            return await Task.FromResult(shoppyCar);
        }

        public async Task<IEnumerable<ShoppyCar>> GetAllShoppyCar()
        {
            var shoppycar = await FindAll();
            return await Task.FromResult(shoppycar
                .Include(ac => ac.Pedidos)
                .OrderBy(ow => ow.Created));
                
        }
        public async Task<ShoppyCar> GetShoppyCarById(Guid shoppyCarId)
        {
            var shoppy = await FindByCondition(shoppyCar => shoppyCar.Id.Equals(shoppyCarId));
          
            return await Task.FromResult(shoppy?.Include(ac => ac.Pedidos!)
                .ThenInclude(b => b.Producto)
                    .FirstOrDefault());
        }

        public async Task<bool> DeleteShoppyCar(ShoppyCar shoppyCar)
        {
            var deleted = await Delete(shoppyCar);
            return await Task.FromResult(deleted);
        }

        public async Task<IEnumerable<ShoppyCar>> GetShoppyCarsByUser(string userId)
        {
            var shoppycars = await FindByCondition(shoppy => shoppy.UserId.Equals(userId));
            return await Task.FromResult(shoppycars?.Include(ac => ac.Pedidos!)?.ThenInclude(b => b.Producto));
        }

        public async Task<bool> IsMyShoppyCar(Guid id, string userId)
        {
            var shoppy = await FindByCondition(shoppyCar => shoppyCar.Id.Equals(id));
            var shoppyIdentity = shoppy.Include(ac => ac.User).FirstOrDefault();
            if(shoppyIdentity?.User != null && shoppyIdentity.User.Id.Equals(userId))
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
    }
}
