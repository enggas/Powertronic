using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{

    [Table ("Clientes")]
    public class Clientes
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Cedula")]
        [Required(ErrorMessage="Es Requerida la Cedula del Cliente")]
        public required string Cedula { get; set; }

        [Column("Nombres")]
        [Required(ErrorMessage="Es Requerido el Nombre del Cliente")]
        public required string NombreCliente { get; set; }

        [Column("Apellidos")]
        [Required(ErrorMessage="Es Requerido el Apellido del Cliente")]
        public required string ApellidoCliente { get; set; }

        [Column("Telefono")]
        [Required(ErrorMessage="Es Requerido el Numero de Telefono")]
        public required string Telefono { get; set; }

        [Column("Gmail")]
        [Required(ErrorMessage="Es Requerido el Gmail del Cliente")]
        public required string Gmail { get; set; }

        [Column("Contraseña")]
        [Required(ErrorMessage="Es Requerida la Contraseña del Cliente")]
        public required string Contraseña { get; set; }

        [Column("Direccion")]
        [Required(ErrorMessage="Es Requerida la Direccion del Cliente")]
        public required string Direccion { get; set; }

        [Column("Estado")]
        public required bool Estado { get; set; }

        [Column("Fecha_Creacion")]
        [Required(ErrorMessage="Es Requerida la Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }


 
    }
}
