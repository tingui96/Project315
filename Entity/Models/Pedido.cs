using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("producto")]
    public class Pedido
    {
        public Guid PedidoId { get; set; }
        [Required(ErrorMessage = "Defina la cantidad")]
        [Range(1,int.MaxValue)]
        public int cantidad { get; set; }
        [ForeignKey(nameof(Producto))]
        public Guid ProductoId { get; set; }
        public Producto? Producto { get; set; }

    }
}
