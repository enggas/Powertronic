using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table("Tipos_Pago")]
    public class TipoPago
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Nombre")]
        [Required(ErrorMessage="El Nombre del Tipo de Pago es Requerido")]
        public required string Nombre { get; set; }

        [Column("Estado")]
        public required bool Estado { get; set; }

    }
}
