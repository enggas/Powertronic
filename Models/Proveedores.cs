using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table ("Proveedores")]
    public class Proveedores
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("CodigoProveedor")]
        [Required(ErrorMessage="El Codigo del Proveedor es Requerido")]
        public required string Codigo { get; set; }

        [Column("NombreProveedor")]
        [Required(ErrorMessage="El Nombre del Proveedor es Requerido")]
        public required string Nombre { get; set; }

        [Column("Telefono")]
        [Required(ErrorMessage="El Numero de Telefono del Proveedor es Requerido")]
        public required string Telefono { get; set; }

        [Column("Direccion")]
        [Required(ErrorMessage="La Direccion del Proveedor es Requerida")]
        public required string Direccion { get; set; }

        [Column("Estado")]
        public required bool Estado { get; set; }


        public ICollection<Adquisicion> adquisiciones { get; set; } = new List<Adquisicion>();
    }
}
