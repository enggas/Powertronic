using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table("Detalle_Reparacion")]
    public class DetalleReparacion
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }


        [Column("Orden_Rep_Id")]
        [ForeignKey(nameof(Orden_Reparacion))]
        required public int OrdenRepId { get; set; }
        public required Orden_Reparacion? Orden_Reparacion { get; set; }

        [Column("Producto_Reparado")]
        [Required(ErrorMessage = "El Producto Reparado es Requerido")]
        public required string ProductoReparado { get; set; }

        [Column("DescripcionRep")]
        [Required(ErrorMessage = "La Descripción de la Reparación es Requerida")]
        public required string DescripcionReparacion { get; set; }



    }
}
