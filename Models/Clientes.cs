using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{

    [Table ("Clientes")]
    public class Clientes
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Es Requerido el Nombre del Cliente")]
        public required string NombreCliente { get; set; }

        [Required(ErrorMessage="Es Requerido el Apellido del Cliente")]
        public required string ApellidoCliente { get; set; }

        [Required(ErrorMessage="Es Requerido el Numero de Telefono")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage="Es Requerido el Gmail del Cliente")]
        public required string Gmail { get; set; }


        [Required(ErrorMessage="Es Requerida la Direccion del Cliente")]
        public required string Direccion { get; set; }

        public required bool Estado { get; set; }

        [Required(ErrorMessage="Es Requerida la Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }


 
    }
}
