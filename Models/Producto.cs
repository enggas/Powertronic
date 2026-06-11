using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;

namespace Powertronic.Models
{
    [Table ("Producto")]
    public class Producto
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("CodigoProducto")]
        [Required(ErrorMessage="El Codigo del Producto es Requerido")]
        public required string Codigo { get; set; }

        [Column("NombreProducto")]
        [Required(ErrorMessage="El Nombre del Producto es Requerido")]
        public required string Nombre { get; set; }

        [Column("PrecioVenta")]
        [Required(ErrorMessage="El Precio de Venta es Requerido")]
        public decimal PrecioVenta { get; set; }

        [Column("PrecioCompra")]
        [Required(ErrorMessage="El Precio de Compra es Requerido")]
        public decimal PrecioCompra { get; set; }

        [Column("Stock")]
        [Required(ErrorMessage="El Stock Disponible es Requerido")]
        public int StockDisponible { get; set; }

        [Column("Imagen")]
        public string ImagenUrl { get; set; } = string.Empty;


        [Column("CategoriaProducto_Id")]
        [ForeignKey(nameof(CategoriaProducto))]
        public required int CategoriaProductoId { get; set; }
        public required CategoriaProducto? CategoriaProducto { get; set; }



        [Column("Proveedores_Id")]
        [ForeignKey(nameof(Proveedores))]
        public int ProveedoresId { get; set; }
        public required Proveedores? Proveedores { get; set; }
        
        [Column("Estado")]
        public required bool Estado { get; set; }

        [Column("Fecha_Creacion")]
        [Required(ErrorMessage="La Fecha de Creacion es Requerida")]
        public DateTime FechaCreacion { get; set; }


        public ICollection<Det_Venta> Detalle_Ventas { get; set; } = new List<Det_Venta>();

    }
}
