using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class ShoppyCarWithDetailDTO
    {       
        public Guid Id { get; set; }  
        public DateTime Created { get; set; }
        public IEnumerable<PedidoDTO>? Pedidos { get; set; }  
        public double Total { get; set; }
    }
}
