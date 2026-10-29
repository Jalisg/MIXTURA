using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIXTURAA.Models
{
    public class DetalleReceta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RecetaId { get; set; }

        [ForeignKey("RecetaId")]
        public virtual Receta? Receta { get; set; }

        [Required]
        public int IngredienteId { get; set; }

        [ForeignKey("IngredienteId")]
        public virtual Ingrediente? Ingrediente { get; set; }

        [Required]
        public decimal CantidadRequerida { get; set; }
    }
}