using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class CategoriaWithProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<ProductoDTO>? Productos { get; set; }
    }
}
