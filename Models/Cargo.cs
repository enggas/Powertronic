using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Cargo")]
    public class Cargo
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("NombreCargo")]
        [Required(ErrorMessage="El Nombre del Cargo es Requerido")]
        public required string NombreCargo { get; set; }

        [Column("Descripcion")]
        [Required(ErrorMessage="La Descripcion del Cargo es Requerida")]
        public required string Descripcion { get; set; }

        [Column("Fecha_Creacion")]
        [Required(ErrorMessage="La Fecha de Creacion es Requerida")]
        public DateTime FechaCreacion { get; set; }

        [Column("Estado")]
        public required bool Estado { get; set; }

        public required List<Empleado> Empleados { get; set; }



    }
}
