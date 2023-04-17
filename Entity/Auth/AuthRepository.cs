using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

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
            var rol = await _roleManager.CreateAsync(role);
            return await Task.FromResult(rol);
        }

        public async Task<IdentityResult> AddRole(string id, string rol)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new Exception();
            var result = await _userManager.AddToRoleAsync(user, rol);

            return await Task.FromResult(result);
        }

        public async Task<IList<string>> GetAllRoles(User user)
        {
               return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersInRole(string rol)
        {
            var users = await _userManager.GetUsersInRoleAsync(rol);
            return await Task.FromResult(users);
        }

        public async Task<User> Login(LoginModel model)
        {
            var userToVerify = await _userManager.FindByNameAsync(model.Usuario) ?? throw new Exception();
            var check = await _userManager.CheckPasswordAsync(userToVerify,model.Password);
            if (check) return await Task.FromResult(userToVerify);
            throw new Exception();
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new User(model.Name) { UserName = model.UserName, Activo = true };

            var result = await _userManager.CreateAsync(user, model.Password);

            var userDb = await _userManager.FindByNameAsync(model.UserName) ?? throw new Exception();       
                       
            await _userManager.AddToRoleAsync(userDb, model.Role);            

            return await Task.FromResult(result);
        }
        public async Task<IdentityResult> Register(RegisterModelUser model)
        {
            var user = new User(model.Name) { UserName = model.UserName, Activo = true };

            var result = await _userManager.CreateAsync(user, model.Password);

            var userDb = await _userManager.FindByNameAsync(model.UserName) ?? throw new Exception();

            await _userManager.AddToRoleAsync(userDb, "Viewer");
            
            return await Task.FromResult(result);
        }

        public async Task<IdentityResult> RemoveRole(string id, string rolname)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new Exception();
            var result = await _userManager.RemoveFromRoleAsync(user, rolname);

            return await Task.FromResult(result);
        }

        public async Task<User> UserExists(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            return await Task.FromResult(user);
        }
        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new Exception();            
            var result = await _userManager.DeleteAsync(user);
            return await Task.FromResult(result);                       
        }

       
    }
}