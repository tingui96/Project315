using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class PedidoDTO
    {
        public Guid Id { get; set; }
        public Guid productoId { get; set; }
        public Guid shoppyCarId { get; set; }
        public ulong cantidad { get; set; }
        public double Total { get; set; }


    }
}
