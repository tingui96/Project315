using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class ProductoForCreationDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Necesita una descripcion del producto")]
        [StringLength(400)]
        public string? Descripcion { get; set; }
        [Required]
        public Guid categoriaId { get; set; }
    }
}
