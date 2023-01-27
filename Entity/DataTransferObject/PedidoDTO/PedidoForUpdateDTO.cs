using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class PedidoForUpdateDTO
    {
        [Required]
        public Guid ShoppyCarId { get; set; }
        [Required]
        public Guid productoId { get; set; }
        [Required(ErrorMessage = "Defina la cantidad")]
        [Range(1, ulong.MaxValue)]
        public ulong cantidad { get; set; }
    }
}
