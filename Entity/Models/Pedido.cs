using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("pedido")]
    public class Pedido
    {
        [Column("PedidoId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Defina la cantidad")]
        [Range(1,ulong.MaxValue)]
        public ulong cantidad { get; set; }

        public double Total
        {
            get {
                if (Producto == null)
                    return 0;
                else                   
                    return Producto.Price * cantidad;
            }
        }
        [ForeignKey(nameof(Producto))]
        public Guid ProductoId { get; set; }
        public Producto? Producto { get; set; }



    }
}
