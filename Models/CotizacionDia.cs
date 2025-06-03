namespace CotizacionesAPI.Models
{
    public class CotizacionDia
    {
        public string? Dia { get; set; }
        public MonedaCotizacion? Dolar { get; set; }
        public MonedaCotizacion? Real { get; set; }
        public MonedaCotizacion? PesoArgentino { get; set; }
        public MonedaCotizacion? Yen { get; set; }
        public MonedaCotizacion? Euro { get; set; }
        public MonedaCotizacion? Libra { get; set; }
    }

    public class MonedaCotizacion
    {
        public string? Compra { get; set; }
        public string? Venta { get; set; }
    }
}
