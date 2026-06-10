using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Det_Venta")]
    public class Det_Venta
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Venta_Prod))]
        public int Venta_ProdId { get; set; }

        [ForeignKey(nameof(Producto))]
        public int ProductoId { get; set; }

        [Required(ErrorMessage="La Cantidad de Producto es Requerida")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage="El Total de la Venta es Requerido")]
        public decimal TotalVenta { get; set; }

        public required Venta_Prod? Venta_Prod { get; set; }
        public required Producto? Producto { get; set; }

    }
}
