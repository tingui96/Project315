using Entities.Auth;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> UsersQueryable();
        Task<IEnumerable<User>> FindAll();
        Task<IQueryable<User>> FindAllInRole(string role);
        Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression);
        Task<User> GetById(string Id);
        void Update(User Entity);
        void Delete(User Entity);
    }
}
