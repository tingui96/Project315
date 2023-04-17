using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;


namespace Contracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> FindAll();
        Task<IdentityRole> Create(IdentityRole Entity);
        Task<IdentityRole> GetById(string Id);
        Task<IdentityRole> FindByCondition(Expression<Func<IdentityRole, bool>> expression);
        Task<bool> Delete(IdentityRole rol);
    }
}
