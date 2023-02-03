using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Entities.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> AddRole(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> AddRole(string id, string rol)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, rol);

            return result;
        }

        public async Task<IList<string>> GetAllRoles(User user)
        {
               return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersInRole(string rol)
        {
             return await _userManager.GetUsersInRoleAsync(rol);
        }

        public async Task<User> Login(LoginModel model)
        {
            var userToVerify = await _userManager.FindByNameAsync(model.Usuario);
            if (userToVerify != null && await _userManager.CheckPasswordAsync(userToVerify,model.Password))
            {
                return userToVerify;
            }
            return null;
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new User() { Name=model.Name,UserName = model.UserName, activo = true };

            var result = await _userManager.CreateAsync(user, model.Password);

            var userDb = await _userManager.FindByNameAsync(model.UserName);
            
            if (result.Succeeded)
            {                
                var roleresult = await _userManager.AddToRoleAsync(userDb, model.Role);
            }

            return result;
        }

        public async Task<IdentityResult> RemoveRole(string id, string rolname)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, rolname);

            return result;
        }

        public async Task<User> UserExists(string Username)
        {
            return await _userManager.FindByNameAsync(Username);
        }
        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);            
            var result = await _userManager.DeleteAsync(user);
            return result;                       
        }
    }
}