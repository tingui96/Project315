using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class UserWithShoppyCarDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<ShoppyCarDTO>? ShoppyCars { get; set; }
    }
}
