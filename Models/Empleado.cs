using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Empleado")]
    public class Empleado
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="El Codigo del Empleado es Requerido")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage="El Nombre del Empleado es Requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage="El Apellido del Empleado es Requerido")]
        public required string Apellido { get; set; }

        [ForeignKey(nameof(Cargo))]
        public int CargoId { get; set; }

        [Required(ErrorMessage="El Telefono del Empleado es Requerido")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage="El Gmail del Empleado es Requerido")]
        public required string Gmail { get; set; }

        [Required(ErrorMessage="La Direccion del Empleado es Requerida")]
        public required string Direccion { get; set; }

        public required bool Estado { get; set; }

        [Required(ErrorMessage="La Fecha de Registro es Requerida")]
        public required DateTime FechaRegistro { get; set; }



    }
}
