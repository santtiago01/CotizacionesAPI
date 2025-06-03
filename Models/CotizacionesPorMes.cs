namespace CotizacionesAPI.Models
{
    public class CotizacionesPorMes
    {
        public string? Mes { get; set; }
        public List<CotizacionDia> Cotizaciones { get; set; } = new();
    }
}
