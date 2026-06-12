namespace Powertronic.Models.ViewModels
{
    public class CarritoItemVM
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal => PrecioVenta * Cantidad;
    }

    public class CarritoViewModel
    {
        public List<Producto> Productos { get; set; } = new();
        public List<CarritoItemVM> Items { get; set; } = new();
        public List<Clientes> Clientes { get; set; } = new();
        public List<TipoPago> TiposPago { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
