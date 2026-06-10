using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Empleado")]
    public class Empleado
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("CodigoEmpleado")]
        [Required(ErrorMessage="El Codigo del Empleado es Requerido")]
        public required string Codigo { get; set; }

        [Column("Cedula")]
        [Required(ErrorMessage = "Es Requerida la Cedula del Empleado")]
        public string Cedula { get; set; }

        [Column("Nombres")]
        [Required(ErrorMessage="El Nombre del Empleado es Requerido")]
        public required string Nombre { get; set; }

        [Column("Apellidos")]
        [Required(ErrorMessage="El Apellido del Empleado es Requerido")]
        public required string Apellido { get; set; }

        [Column("Cargo_Id")]
        [ForeignKey(nameof(Cargo))]
        public int CargoId { get; set; }
        public required Cargo Cargo { get; set; }

        [Column("Telefono")]
        [Required(ErrorMessage="El Telefono del Empleado es Requerido")]
        public required string Telefono { get; set; }

        [Column("Gmail")]
        [Required(ErrorMessage="El Gmail del Empleado es Requerido")]
        public required string Gmail { get; set; }

        [Column("Direccion")]
        [Required(ErrorMessage="La Direccion del Empleado es Requerida")]
        public required string Direccion { get; set; }

        [Column("Estado")]
        public required bool Estado { get; set; }

        [Column("Fecha_Creacion")]
        [Required(ErrorMessage="La Fecha de Registro es Requerida")]
        public required DateTime FechaRegistro { get; set; }


        public ICollection<Despacho> despachos { get; set; } = new List<Despacho>();
        public ICollection<Orden_Reparacion> orden_Reparaciones { get; set; } = new List<Orden_Reparacion>();

    }
}
