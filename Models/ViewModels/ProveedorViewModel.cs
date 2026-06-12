namespace Powertronic.Models.ViewModels
{
    public class ProveedorViewModel
    {

        public Proveedores proveedores { get; set; } = null!;

        public List<Producto> productos { get; set; } = new List<Producto>();

    }
}
