using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Proveedores")]
    public class Proveedores
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage="El Codigo del Proveedor es Requerido")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage="El Nombre del Proveedor es Requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage="El Numero de Telefono del Proveedor es Requerido")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage="La Direccion del Proveedor es Requerida")]
        public required string Direccion { get; set; }

        public required bool Estado { get; set; }


    }
}
