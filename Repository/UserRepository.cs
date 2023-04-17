using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;

        public UserRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<bool> Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<User>> FindAll()
        {
            var users = await _context.Users.ToListAsync();
            return await Task.FromResult(users);
        }

        public async Task<User> GetById(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return await Task.FromResult(user);
        }
        public async Task<User> Update(User Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
            return await Task.FromResult(Entity);
        }
        public async Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression)
        {
            var users  = await _context.Set<User>().Where(expression).ToListAsync();
            return await Task.FromResult(users);
        }
        public async Task<IQueryable<User>> UsersQueryable()
        {
            return await Task.FromResult(_context.Users.AsQueryable());
        }
        public async Task<IQueryable<User>> FindAllInRole(string roleId)
        {
            var userRole = await _context.UserRoles.Where(u => u.RoleId.Equals(roleId)).Select(ur => ur.UserId).ToListAsync();
            var users = _context.Users.Where(u => userRole.Contains(u.Id));                        
            return await Task.FromResult(users);
        }
    }
}
