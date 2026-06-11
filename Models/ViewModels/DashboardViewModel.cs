namespace Powertronic.Models.ViewModels
{
    public class DashboardViewModel
    {

        public List<Empleado> Empleados { get; set; } = new();

        public decimal TotalGanancias { get; set; }

        public decimal TotalPerdidas { get; set; }

        public int VentasEfectivo { get; set; }

        public int VentasTarjeta { get; set; }


    }
}
