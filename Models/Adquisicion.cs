using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Adquisicion")]
    public class Adquisicion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El Numero de Documento es Requerido")]
        public required string NumeroDocumento { get; set; }


        [ForeignKey (nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }



        [ForeignKey(nameof(Proveedor))]
        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }


        [Required(ErrorMessage ="El Total de la Adquisicion es Requerida")]
        public decimal Total { get; set; }



        [Required(ErrorMessage = "La Fecha de Adquisición es Requerida")]
        public DateTime FechaAdquisicion { get; set; }





    }
}
