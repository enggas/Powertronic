using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Adquisicion")]
    public class Adquisicion
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Num_Documento")]
        [Required(ErrorMessage ="El Numero de Documento es Requerido")]
        public required string NumeroDocumento { get; set; }

        [Column("Empleado_Id")]
        [ForeignKey (nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }


        [Column("Proveedor_Id")]
        [ForeignKey(nameof(Proveedor))]
        public int ProveedorId { get; set; }
        public Proveedores? Proveedor { get; set; }

        [Column("Total")]
        [Required(ErrorMessage ="El Total de la Adquisicion es Requerida")]
        public decimal Total { get; set; }


        [Column("Fecha")]
        [Required(ErrorMessage = "La Fecha de Adquisición es Requerida")]
        public DateTime FechaAdquisicion { get; set; }



        public ICollection<Detalle_Adquisicion> Detalles { get; set; }= new List<Detalle_Adquisicion>();

    }
}
