using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class PedidoForCreationDTO
    {
        [Required]
        public Guid ShoppyCarId { get; set; }
        [Required]
        public Guid productoId { get; set; }
        public Producto? producto { get; set; }
        [Required(ErrorMessage = "Defina la cantidad")]
        [Range(1, ulong.MaxValue)]
        public ulong cantidad { get; set; }
        public double Total
        {
            get
            {
                if (producto == null)
                    return 0;
                else
                    return producto.Price * cantidad;
            }
        }
    }

}
