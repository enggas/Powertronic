using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powertronic.Models
{
    [Table("Pago_Tarjeta")]
    public class Pago_Tarjeta
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Despacho_Id")]
        [ForeignKey(nameof(Despacho))]
        public int DespachoId { get; set; }
        public Despacho? Despacho { get; set; } = null!;

        [Column("MarcaTarjeta")]
        [Required(ErrorMessage="La Marca de la Tarjeta es Requerida")]
        public required string MarcaTarjeta { get; set; }

        [Column("Ultimos4")]
        [Required(ErrorMessage = "El Numero de Tarjeta es Requerido")]
        public required string Ultimos4 { get; set; }

        [Column("CodigoAutorizacion")]
        [Required(ErrorMessage="El Codigo de Autorizacion es Necesario")]
        public required string CodigoAutorizacion { get; set; }

        [Column("Monto")]
        [Required(ErrorMessage="El Monto es Requerido")]
        public required decimal Monto { get; set; }

        [Column("FechaPago")]
        [Required(ErrorMessage="La Fecha de Creacion es Necesaria")]
        public DateTime FechaPago { get; set; }


        

    }
}
