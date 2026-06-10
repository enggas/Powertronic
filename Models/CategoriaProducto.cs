using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table("CategoriaProducto")]
    public class CategoriaProducto
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Es Requerida El Nombre de la Categoria")]
        public required string NombreCategoria { get; set; }

        public required bool Estado { get; set; }

        public required List<Producto> Productos { get; set; }


    }
}
