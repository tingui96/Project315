using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Models.ShoppyCar;


namespace Entities.DataTransferObject
{
    public class ShoppyCarForCreationDTO
    {
        public DateTime Created { get { return DateTime.Now; } }
        public Status Status{ get { return Status.Creado; } }
        public string? userId { get; set; }
    }
}
