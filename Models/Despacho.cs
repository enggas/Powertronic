using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Despacho")]
    public class Despacho
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Num_Factura")]
        [Required(ErrorMessage="El Numero de Factura es Requerida")]
        public required string NumeroFactura { get; set; }

        [Column("Cliente_Id")]
        [ForeignKey(nameof(Clientes))]
        public int ClientesId { get; set; }
        public required Clientes? Clientes { get; set; }

        [Column("Empleado_Id")]
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public required Empleado? Empleado { get; set; }

        [Column("Venta_Prod_Id")]
        [ForeignKey(nameof(Venta_Prod))]
        public int Venta_ProdId { get; set; }
        public required Venta_Prod? Venta_Prod { get; set; }

        [Column("Total")]
        [Required(ErrorMessage="El Total de la Factura es Requerida")]
        public decimal TotalFactura { get; set; }


        [Column("Fecha")]
        [Required(ErrorMessage="La Fecha de la Factura es requerida")]
        public DateTime FechaFactura { get; set; }

        [Column("TipoPago_Id")]
        [ForeignKey(nameof(TipoPago))]
        public required int TipoPagoId { get; set; }


        public ICollection<Orden_Reparacion> orden_Reparacions { get; set; } = new List<Orden_Reparacion>();

 

    }
}
