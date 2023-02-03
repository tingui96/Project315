using Contracts;
using Entities;
using Entities.Auth;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;

        public UserRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public void Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }
        public async Task<IEnumerable<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public void Update(User Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
        }
        public async Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return await _context.Set<User>().Where(expression).ToListAsync();
        }
        public IQueryable<User> UsersQueryable()
        {
            return _context.Users.AsQueryable();
        }
        public async Task<IQueryable<User>> FindAllInRole(string roleId)
        {
            var userRole = await _context.UserRoles.Where(u => u.RoleId.Equals(roleId)).ToListAsync();
            var users = from u in this._context.Users
                        where (from r in this._context.UserRoles
                               where r.RoleId == roleId
                               select r.UserId).Contains(u.Id)
                        select u;
            return users;
        }
    }
}
