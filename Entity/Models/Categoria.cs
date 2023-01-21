﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("categoria")]
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [StringLength(60, ErrorMessage = "El nombre no puede contener mas de 60 caracteres")]
        public string? Name { get; set; }
    }
}
