namespace Powertronic.Models.ViewModels
{
    public class OrdenReparacionViewModel
    {

        public Orden_Reparacion Orden { get; set; } = null!;

        public string ClienteNombre { get; set; } = "";

        public string EmpleadoNombre { get; set; } = "";

        public string NumeroFactura { get; set; } = "";

        public List<DetalleReparacion> Detalles { get; set; } = new();




    }
}
