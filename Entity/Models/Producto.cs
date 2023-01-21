using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    [Table("producto")]
    public class Producto
    {
        public Guid ProductoId { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [StringLength(60, ErrorMessage = "El nombre no puede contener mas de 60 caracteres")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Precio es requerido")]
        [Range(0.01, double.MaxValue ,ErrorMessage = "El precio debe ser mayor que 0")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Necesita una descripcion del producto")]
        [StringLength(400)]
        public string? Descripcion { get; set; }
        [Required]
        [ForeignKey(nameof(Categoria))]
        public Guid CategoriaId { get; set; }
        public Categoria? categoria { get; set; }
    }
}
