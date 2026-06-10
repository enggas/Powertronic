using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Orden_Reparacion")]
    public class Orden_Reparacion
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Factura_Id")]
        [ForeignKey(nameof(Despacho))]
        public int DespachoId { get; set; }
        public required Despacho? Despacho { get; set; }

        [Column("Empleado_Id")]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public required Empleado? Empleado { get; set; }

        [Column("Det_Reparacion")]
        [Required(ErrorMessage="Los Detalles de la Reparacion son requeridos")]
        public required string DetallesReparacion { get; set; }

        [Column("Costo")]
        [Required(ErrorMessage="El Costo de la Reparacion es Requerido")]
        public decimal CostoReparacion { get; set; }

        [Column("Fecha_Orden")]
        [Required(ErrorMessage="La Fecha de Orden es Requerida")]
        public DateTime FechaOrden { get; set; }

        [Column("Fecha_Entrega")]
        [Required(ErrorMessage="La Fecha de Entrega es Requerida")]
        public DateTime FechaEntrega { get; set; }

        [Column("EstadoEntrega")]
        public required bool EstadoEntrega { get; set; }


      

    }
}
