﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Models.ShoppyCar;

namespace Entities.DataTransferObject
{
    public class ShoppyCarForUpdateDTO
    {
        public Guid Id { get; set; }
        public Status Estado { get; set; }
    }
}
