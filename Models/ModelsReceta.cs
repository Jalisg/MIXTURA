using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIXTURAA.Models
{
    public class Receta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la receta es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        // Propiedades de la receta
        public string? Molde { get; set; }
        public decimal PesoFinal { get; set; }
        public decimal CostoTotalProduccion { get; set; }

        // Propiedad de privacidad: false = Privada (solo el creador), true = Pública (comunidad)
        public bool EsPublica { get; set; } = false;

        // Usuario creador
        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Creador { get; set; }

        // Relación ajustada a DetalleReceta
        public virtual ICollection<DetalleReceta> Detalles { get; set; } = new List<DetalleReceta>();
    }
}