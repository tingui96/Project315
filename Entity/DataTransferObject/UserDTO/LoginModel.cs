using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class LoginModel
    {
        [Required]
        public string? Usuario { get; set;}
        [Required,StringLength(20)]
        public string? Password { get; set;}
    }
}
