using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Despacho")]
    public class Despacho
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="El Numero de Factura es Requerida")]
        public required string NumeroFactura { get; set; }

        [ForeignKey(nameof(Clientes))]
        public int ClientesId { get; set; }

        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }

        [ForeignKey(nameof(Venta_Prod))]
        public int Venta_ProdId { get; set; }

        [ForeignKey(nameof(Orden_Reparacion))]
        public int Orden_ReparacionId { get; set; }

        [Required(ErrorMessage="El Total de la Factura es Requerida")]
        public decimal TotalFactura { get; set; }


        [Required(ErrorMessage="La Fecha de la Factura es requerida")]
        public DateTime FechaFactura { get; set; }
        
         public required Clientes? Clientes { get; set; }
         public required Empleado? Empleado { get; set; }
         public required Venta_Prod? Venta_Prod { get; set; }
         public required Orden_Reparacion? Orden_Reparacion { get; set; }

    }
}
