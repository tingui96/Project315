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

        public IEnumerable<Categoria> GetAllCategoria()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }
        public Categoria GetCategoriaById(Guid categoriaId)
        {
            return FindByCondition(categoria => categoria.Id.Equals(categoriaId))
                    .FirstOrDefault();
        }

        public void DeleteCategoria(Categoria categoria)
        {
            Delete(categoria);
        }
        public Categoria GetCategoriaWithDetails(Guid categoriaId)
        {
            return FindByCondition(categoria => categoria.Id.Equals(categoriaId))
                .Include(ac => ac.Productos)
                .FirstOrDefault();
        }
    }
}
