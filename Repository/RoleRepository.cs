using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RepositoryContext _context;

        public RoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IdentityRole> Create(IdentityRole Entity)
        {
            _context.Roles.Add(Entity);
            return await Task.FromResult(Entity);
        }

        public async Task<IEnumerable<IdentityRole>> FindAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetById(string Id)
        {
            return await _context.Roles.FindAsync(Id);
        }
        public async Task<IdentityRole> FindByCondition(Expression<Func<IdentityRole, bool>> expression)
        {
            return await _context.Set<IdentityRole>().Where(expression).FirstOrDefaultAsync();
        }
        public async Task<bool> Delete(IdentityRole rol)
        {
            _context.Roles.Remove(rol);   
            return await Task.FromResult(true);
        }
    }
}
