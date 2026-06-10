using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Detalle_Adquisicion")]
    public class Detalle_Adquisicion
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Adquisicion))]
        public int AdquisicionId { get; set; } 
        public Adquisicion? Adquisicion { get; set; } = null!;

        [ForeignKey(nameof(Producto))]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; } = null!;


        [Required(ErrorMessage="El Precio de Adquisicion es Requerido")]
        public decimal PrecioAdquisicion { get; set; }

        [Required(ErrorMessage="El Stock Adquirido es Requerido")]
        public int Stock { get; set; }

        [Required(ErrorMessage="El Total de la Adquisicion es Requerido")]
        public decimal Total { get; set; }




    }
}
