namespace CotizacionesAPI.Models
{
    public class CotizacionDia
    {
        public string? Dia { get; set; }
        public MonedaCotizacion? Dolar { get; set; }
    }

    public class MonedaCotizacion
    {
        public string? Compra { get; set; }
        public string? Venta { get; set; }
    }
}
