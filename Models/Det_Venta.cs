using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Det_Venta")]
    public class Det_Venta
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Venta_Prod_Id")]
        [ForeignKey(nameof(Venta_Prod))]
        public int Venta_ProdId { get; set; }
        public required Venta_Prod? Venta_Prod { get; set; } = null!;

        [Column("Producto_Id")]
        [ForeignKey(nameof(Producto))]
        public int ProductoId { get; set; } 
        public required Producto? Producto { get; set; } = null!;

        [Column("Cantidad")]
        [Required(ErrorMessage="La Cantidad de Producto es Requerida")]
        public int Cantidad { get; set; }

        [Column("Total")]
        [Required(ErrorMessage="El Total de la Venta es Requerido")]
        public decimal TotalVenta { get; set; }

       
  

    }
}
