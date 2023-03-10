using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAllCategoria();
        Task<Categoria> GetCategoriaById(Guid categoriaId);
        void CreateCategoria(Categoria categoria);
        void UpdateCategoria(Categoria categoria);
        void DeleteCategoria(Categoria categoria);
        Task<Categoria> GetCategoriaWithDetails(Guid categoriaId);
    }
}
