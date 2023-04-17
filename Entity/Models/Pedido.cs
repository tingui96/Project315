using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("pedido")]
    public class Pedido:IEntity
    {
        [Column("PedidoId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Defina la cantidad")]
        [Range(1,ulong.MaxValue)]
        public ulong Cantidad { get; set; }

        public double Total
        {
            get {
                if (Producto == null)
                    return 0;
                else                   
                    return Producto.Price * Cantidad;
            }
        }
        [ForeignKey(nameof(Producto))]
        public Guid ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [ForeignKey(nameof(ShoppyCar))]
        public Guid ShoppyCarId { get; set; }
        public ShoppyCar? ShoppyCar { get; set; }

        public Guid GetId() { return Id; }
    }
}
