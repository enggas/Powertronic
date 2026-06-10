using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Orden_Reparacion")]
    public class Orden_Reparacion
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Despacho))]
        public int DespachoId { get; set; }

        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage="Los Detalles de la Reparacion son requeridos")]
        public required string DetallesReparacion { get; set; }

        [Required(ErrorMessage="El Costo de la Reparacion es Requerido")]
        public decimal CostoReparacion { get; set; }

        [Required(ErrorMessage="La Fecha de Orden es Requerida")]
        public DateTime FechaOrden { get; set; }

        [Required(ErrorMessage="La Fecha de Entrega es Requerida")]
        public DateTime FechaEntrega { get; set; }

        public required bool EstadoEntrega { get; set; }


        public required Empleado? Empleado { get; set; }

        public required Despacho? Despacho { get; set; }
    }
}
