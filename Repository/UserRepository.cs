using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                    .FirstOrDefault();
        }

        public User GetUserByName(string name)
        {
            return FindByCondition(user => user.Name.Equals(name)).FirstOrDefault();
        }

        public User GetUserWithDetails(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .Include(sc => sc.ShoppyCars)
                .FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
