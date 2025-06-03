using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using CotizacionesAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

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
            {
                return NotFound("No se encontró la tabla de cotizaciones.");
            }

            var rows = table.SelectNodes(".//tr");
            if (rows == null || rows.Count < 3)
            {
                return NotFound("No se encontraron filas de datos en la tabla.");
            }

            // Las dos primeras filas son encabezados
            var dataRows = rows.Skip(2);

            var cotizaciones = new List<CotizacionDia>();

            foreach (var row in dataRows)
            {
                var cells = row.SelectNodes("td");
                if (cells == null || cells.Count < 13)
                {
                    continue;
                }

                var cotizacion = new CotizacionDia
                {
                    Dia = cells[0].InnerText.Trim(),
                    Dolar = new MonedaCotizacion
                    {
                        Compra = cells[1].InnerText.Trim(),
                        Venta = cells[2].InnerText.Trim()
                    },
                    Real = new MonedaCotizacion
                    {
                        Compra = cells[3].InnerText.Trim(),
                        Venta = cells[4].InnerText.Trim()
                    },
                    PesoArgentino = new MonedaCotizacion
                    {
                        Compra = cells[5].InnerText.Trim(),
                        Venta = cells[6].InnerText.Trim()
                    },
                    Yen = new MonedaCotizacion
                    {
                        Compra = cells[7].InnerText.Trim(),
                        Venta = cells[8].InnerText.Trim()
                    },
                    Euro = new MonedaCotizacion
                    {
                        Compra = cells[9].InnerText.Trim(),
                        Venta = cells[10].InnerText.Trim()
                    },
                    Libra = new MonedaCotizacion
                    {
                        Compra = cells[11].InnerText.Trim(),
                        Venta = cells[12].InnerText.Trim()
                    }
                };

                cotizaciones.Add(cotizacion);
            }

            return Ok(cotizaciones);
        }
    }
}
