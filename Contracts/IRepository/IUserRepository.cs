using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User GetUserWithDetails(Guid userId);
    }
}
