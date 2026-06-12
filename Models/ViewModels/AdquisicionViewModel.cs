namespace Powertronic.Models.ViewModels
{
    public class AdquisicionViewModel
    {


        public Adquisicion Adquisicion { get; set; } = null!;

        public List<Detalle_Adquisicion> Detalles { get; set; }= new();


    }
}
