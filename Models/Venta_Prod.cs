using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table("Venta_Prod")]
    public class Venta_Prod
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="El Total de la Venta es Requerido")]
        public decimal TotalVenta { get; set; }

        [Required(ErrorMessage="La Fecha de Creacion de la Venta es Requerida")]
        public DateTime FechaCreacion { get; set; }

        public required List<Despacho>? Despacho { get; set; }




    }
}
