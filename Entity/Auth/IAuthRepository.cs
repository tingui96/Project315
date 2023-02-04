using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Auth
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(RegisterModel model); 
        Task<IdentityResult> Register(RegisterModelUser model);
        Task<User> Login(LoginModel model);
        Task<IdentityResult> AddRole(IdentityRole role);
        //Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        
        //Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        //void ForgotPassword(ForgotPasswordModel model);
        Task<User> UserExists(string Username);
        Task<IList<string>> GetAllRoles(User user);
        Task<IdentityResult> AddRole(string id, string rolname);
        Task<IdentityResult> RemoveRole(string id, string rolname);
        Task<IdentityResult> DeleteUser(string id);
        Task<IEnumerable<User>> GetUsersInRole(string rol);
    }
}
