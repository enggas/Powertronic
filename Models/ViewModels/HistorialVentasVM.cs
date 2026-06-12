namespace Powertronic.Models.ViewModels
{
    public class HistorialVentasVM
    {

        public Venta_Prod Venta { get; set; } = null!;

        public string ClienteNombre { get; set; } = "";

        public string NumeroFactura { get; set; } = "";

        public int TipoPagoId { get; set; }

        public string TipoPagoNombre { get; set; } = "";

        public string? MarcaTarjeta { get; set; }

        public string? Ultimos4 { get; set; }

        public decimal? MontoTarjeta { get; set; }

        public DateTime? FechaPagoTarjeta { get; set; }

    }
}
