using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> UsersQueryable();
        Task<IEnumerable<User>> FindAll();
        Task<IQueryable<User>> FindAllInRole(string role);
        Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression);
        Task<User> GetById(string Id);
        Task<User> Update(User Entity);
        Task<bool> Delete(User Entity);
    }
}
