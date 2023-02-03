using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("shoppycar")]
    public class ShoppyCar:IEntity
    {
        [Column("ShoppyCarId")]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public enum Status { Creado, Pagado }
        public Status Estado { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
        public double Total
        {
            get
            {
                if (Pedidos == null)
                    return 0;
                else
                    return Pedidos.Sum(obj => obj.Total);
            }
        }
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        public Guid GetId() { return Id; }
    }
}
