using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;

namespace Powertronic.Models
{
    [Table ("Producto")]
    public class Producto
    {

        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage="El Codigo del Producto es Requerido")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage="El Nombre del Producto es Requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage="El Precio de Venta es Requerido")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage="El Precio de Compra es Requerido")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage="El Stock Disponible es Requerido")]
        public int StockDisponible { get; set; }

        public string ImagenUrl { get; set; } = string.Empty;


        [ForeignKey(nameof(CategoriaProducto))]
        public string CategoriaProducto { get; set; }

        [ForeignKey(nameof(Proveedores))]
        public int ProveedoresId { get; set; }

        public required bool Estado { get; set; }

        [Required(ErrorMessage="La Fecha de Creacion es Requerida")]
        public DateTime FechaCreacion { get; set; }


        public ICollection<Det_Venta> Detalle_Ventas { get; set; } = new List<Det_Venta>();

    }
}
