using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> FindAll();
        void Create(IdentityRole Entity);
        Task<IdentityRole> GetById(string Id);
        Task<IdentityRole> FindByCondition(Expression<Func<IdentityRole, bool>> expression);
        void Delete(IdentityRole rol);
    }
}
