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

        public IEnumerable<ShoppyCar> GetAllShoppyCar()
        {
            return FindAll()
                .OrderBy(ow => ow.Created)
                .ToList();
        }
        public ShoppyCar GetShoppyCarById(Guid shoppyCarId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return FindByCondition(shoppyCar => shoppyCar.Id.Equals(shoppyCarId))
                    .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void DeleteShoppyCar(ShoppyCar shoppyCar)
        {
            Delete(shoppyCar);
        }
        public ShoppyCar GetShoppyCarWithDetails(Guid shoppyCarId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return FindByCondition(shoppyCar => shoppyCar.Id.Equals(shoppyCarId))
                .Include(ac => ac.Pedidos)
                .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
