using Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RepositoryContext _context;

        public RoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(IdentityRole Entity)
        {
            _context.Roles.Add(Entity);
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
        public async void Delete(IdentityRole rol)
        {
            _context.Roles.Remove(rol);           
        }
    }
}
