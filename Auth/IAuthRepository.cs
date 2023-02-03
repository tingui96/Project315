using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(RegisterModel model);
        Task<IdentityResult> RegisterEmployee(RegisterEmployeeModel model);
        Task<User> Login(LoginModel model);
        Task<IdentityResult> AddRole(IdentityRole role);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        //Task<IdentityResult> SetPassword(SetPasswordModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        void ForgotPassword(ForgotPasswordModel model);
        Task<User> UserExists(string Username);
        Task<IList<string>> GetAllRoles(User user);
        Task<IdentityResult> AddRole(string id, string rolname);
        Task<IdentityResult> RemoveRole(string id, string rolname);
        Task<IEnumerable<User>> GetUsersInRole(string rol);

        Task<User> Register(User user, string password);
        Task<User> Login(string user, string password);
        Task<bool> Existe(string user);
    }
}
