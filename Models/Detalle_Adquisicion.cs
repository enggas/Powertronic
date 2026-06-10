using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Detalle_Adquisicion")]
    public class Detalle_Adquisicion
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Adquisicion_Id")]
        [ForeignKey(nameof(Adquisicion))]
        public int AdquisicionId { get; set; } 
        public Adquisicion? Adquisicion { get; set; } = null!;

        [Column("Producto_Id")]
        [ForeignKey(nameof(Producto))]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; } = null!;


        [Column("PrecioAdquisicion")]
        [Required(ErrorMessage="El Precio de Adquisicion es Requerido")]
        public decimal PrecioAdquisicion { get; set; }

        [Column("Stock")]
        [Required(ErrorMessage="El Stock Adquirido es Requerido")]
        public int Stock { get; set; }

        [Column("Total")]
        [Required(ErrorMessage="El Total de la Adquisicion es Requerido")]
        public decimal Total { get; set; }




    }
}
