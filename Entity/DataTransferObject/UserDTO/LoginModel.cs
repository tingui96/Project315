using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class LoginModel
    {
        [Required]
        public string Usuario { get; set;}
        [Required,StringLength(20)]
        public string Password { get; set;}

        public LoginModel(string usuario, string password)
        {
            Usuario = usuario;
            Password = password;
        }
    }
}
