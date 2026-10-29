using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIXTURAA.Models
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal CantidadPresentacion { get; set; }

        [Required]
        [StringLength(20)]
        public string UnidadMedida { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCompra { get; set; }

        [NotMapped]
        public decimal CostoPorUnidad => CantidadPresentacion > 0 ? PrecioCompra / CantidadPresentacion : 0;
    }
}