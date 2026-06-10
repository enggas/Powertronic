using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Cargo")]
    public class Cargo
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage="El Nombre del Cargo es Requerido")]
        public string NombreCargo { get; set; }


        [Required(ErrorMessage="La Descripcion del Cargo es Requerida")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage="La Fecha de Creacion es Requerida")]
        public DateTime FechaCreacion { get; set; }


        public required bool Estado { get; set; }

        public required List<Empleado> Empleados { get; set; }



    }
}
