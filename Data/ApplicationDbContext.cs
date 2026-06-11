using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Powertronic.Models;

namespace Powertronic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }

        public DbSet<Adquisicion> Adquisicion { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Despacho> Despacho { get; set; }
        public DbSet<Det_Venta> Det_Venta{ get; set; }
        public DbSet<Detalle_Adquisicion> Detalle_Adquisicion { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Orden_Reparacion> Orden_Reparacion { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Venta_Prod> Venta_Prod { get; set; }
        public DbSet<TipoPago> TiposPago { get; set; }
        public DbSet<Pago_Tarjeta> PagosTarjeta { get; set; } 


    }
}
