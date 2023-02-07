using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Models.ShoppyCar;

namespace Entities.DataTransferObject
{
    public class ShoppyCarDTO
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Status Status { get; set; }
        public double Total  { get; set; }
    }
}
