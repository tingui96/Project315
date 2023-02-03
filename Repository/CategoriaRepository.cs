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

        public void CreateCategoria(Categoria categoria)
        {
            Create(categoria);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            Update(categoria);
        }

        public async Task <IEnumerable<Categoria>> GetAllCategoria()
        {
            var categorias = await FindAll();
            return categorias
                .OrderBy(ow => ow.Name)
                .ToList();
        }
        public async Task<Categoria> GetCategoriaById(Guid categoriaId)
        {
            var categoria = await FindByCondition(categoria => categoria.Id.Equals(categoriaId));
            return categoria.FirstOrDefault();
        }

        public void DeleteCategoria(Categoria categoria)
        {
            Delete(categoria);
        }
        public async Task<Categoria> GetCategoriaWithDetails(Guid categoriaId)
        {
            var categoria = await FindByCondition(categoria => categoria.Id.Equals(categoriaId));
            return categoria
                .Include(ac => ac.Productos)
                .FirstOrDefault();
        }
    }
}
