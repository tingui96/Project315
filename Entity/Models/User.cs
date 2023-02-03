using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("user")]
    public class User : IdentityUser, IEntity
    {
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [Required]
        
        public bool activo { get; set; }
        public ICollection<ShoppyCar>? ShoppyCars { get; set;}

        public Guid GetId()
        {
                return Guid.Parse(Id);
        }
    }    

}
