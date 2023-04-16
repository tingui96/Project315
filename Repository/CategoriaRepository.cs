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
    public class CategoriaRepository : RepositoryBase<Categoria> , ICategoriaRepository
    {
        public CategoriaRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Categoria> CreateCategoria(Categoria categoria)
        {
            categoria = await Create(categoria);
            return await Task.FromResult(categoria);
        }

        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            categoria = await Update(categoria);
            return await Task.FromResult(categoria);
        }

        public async Task <IEnumerable<Categoria>> GetAllCategoria()
        {
            var categorias = await FindAll();
            return await Task.FromResult(categorias
                .OrderBy(ow => ow.Name));
        }
        public async Task<Categoria> GetCategoriaById(Guid categoriaId)
        {
            var categoria = await FindByCondition(categoria => categoria.Id.Equals(categoriaId));
            return await Task.FromResult(categoria.FirstOrDefault());
        }

        public async Task<bool> DeleteCategoria(Categoria categoria)
        {
            var deleted = await Delete(categoria);
            return await Task.FromResult(deleted);
        }
        public async Task<Categoria> GetCategoriaWithDetails(Guid categoriaId)
        {
            var categoria = await FindByCondition(categoria => categoria.Id.Equals(categoriaId));
            var categoriaDetails = categoria
                .Include(ac => ac.Productos)
                .FirstOrDefault();
            return await Task.FromResult(categoriaDetails);
        }
    }
}
