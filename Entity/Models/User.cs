using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("user")]
    public class User : IdentityUser, IEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        
        public bool Activo { get; set; }
        public ICollection<ShoppyCar>? ShoppyCars { get; set;}

        public Guid GetId()
        {
                return Guid.Parse(Id);
        }
        public User(string name)
        {
            Name = name;
        }
    }    

}
