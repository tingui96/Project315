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
    public class User :  IEntity
    {
        [Column("UserId")]
        public Guid Id { get; set; }
        [Required, StringLength(20)]
        public string? Usuario { get; set; }
        [Required,StringLength(50)]
        public string? Name { get; set; }
        [Required,StringLength(20)]
        public string? Password { get; set; }
        public string? Rol { get; set; }
        public ICollection<ShoppyCar>? ShoppyCars { get; set;}
    }    

}
