using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using CotizacionesAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CotizacionesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var url = "https://www.dnit.gov.py/web/portal-institucional/cotizaciones";
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);

            var table = doc.DocumentNode.SelectSingleNode("//table");
            if (table == null)
                return NotFound("No se encontró la tabla de cotizaciones.");

            var rows = table.SelectNodes(".//tr");
            if (rows == null || rows.Count < 3)
                return NotFound("No se encontraron filas de datos en la tabla.");

            var dataRows = rows.Skip(2);
            var cotizaciones = new List<CotizacionDia>();

            foreach (var row in dataRows)
            {
                var cells = row.SelectNodes("td");
                if (cells == null || cells.Count < 3)
                    continue;

                var diaTexto = cells[0].InnerText.Trim();

                if (!int.TryParse(diaTexto, out int dia))
                    continue;

                // Crea fecha usando día/mes/año actual
                var hoy = DateTime.Today;
                var fecha = new DateTime(hoy.Year, hoy.Month, dia);

                var compra = cells[1].InnerText.Trim();
                var venta = cells[2].InnerText.Trim();

                cotizaciones.Add(new CotizacionDia
                {
                    Dia = fecha.ToString("dd/MM/yyyy"),
                    Fecha = fecha,
                    Dolar = new MonedaCotizacion
                    {
                        Compra = compra,
                        Venta = venta
                    }
                });
            }

            // Agrupar por mes actual
            var mesActual = DateTime.Today.ToString("MMMM yyyy", new CultureInfo("es-ES"));

            var resultado = new CotizacionesPorMes
            {
                Mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mesActual),
                Cotizaciones = cotizaciones.OrderBy(c => c.Fecha).ToList()
            };

            return Ok(resultado);
        }
    }
}