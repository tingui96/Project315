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
        Task<Categoria> CreateCategoria(Categoria categoria);
        Task<Categoria> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(Categoria categoria);
        Task<Categoria> GetCategoriaWithDetails(Guid categoriaId);
    }
}
