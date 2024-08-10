using INTITUTO1.Server.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/PdfReport")]
    public class PdfReportController : ControllerBase
    {
        private readonly Context _context;

        public PdfReportController(Context context)
        {
            _context = context;
        }

        //get pdf
        [HttpGet]
        public async Task<IActionResult> GetAlumnosReport()
        {
            //obtener la lista de alumnos junto con sus notas desde la base de datos
            var alumnos = await _context.alumnos
                .Include(a => a.DivsionCiclosMateriaAlumnos)
                .ThenInclude(d => d.Notas)
                .ThenInclude(n => n.TipoEvaluacion)
                .ToListAsync();

            //verificar si hay alumnos en la lista
            if (alumnos == null || !alumnos.Any())
            {
                return NotFound("No se encontraron alumnos.");
            }

            //generar el PDF usando el método que ya definiste
            var pdfBytes = GeneratePdf(alumnos);

            //devolver el pdf como un archivo
            return File(pdfBytes, "application/pdf", "ReporteAlumnos.pdf");
        }

        private byte[] GeneratePdf(List<Alumnos> alumnos)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(72); // 1 punto = 72 pulgadas (???)
                    page.Header().Text("Reporte de Alumnos").Bold().FontSize(20);

                    foreach (var alumno in alumnos)
                    {
                        page.Content().Text($"Nombre: {alumno.Nombre} {alumno.Apellido}");
                        page.Content().Text($"DNI: {alumno.DNI_Alum}");
                        page.Content().Text($"Fecha de Nacimiento: {alumno.Fecha_Nac:dd/MM/yyyy}");
                        page.Content().Text($"Estado: {(alumno.Estado ? "Activo" : "Inactivo")}");
                        page.Content().PaddingBottom(10);

                        foreach (var divCicMatAlum in alumno.DivsionCiclosMateriaAlumnos)
                        {
                            page.Content().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(200);
                                    columns.ConstantColumn(100);
                                    columns.ConstantColumn(100);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Asignatura").Bold();
                                    header.Cell().Text("Nota").Bold();
                                    header.Cell().Text("Fecha").Bold();
                                });

                                foreach (var nota in divCicMatAlum.Notas)
                                {
                                    //table.Cell().Text(nota.TipoEvaluacion.Nombre); REVISAR HEHEHE
                                    table.Cell().Text(nota.Nota.ToString());
                                    table.Cell().Text(nota.Fecha.ToString("dd/MM/yyyy"));
                                }
                            });

                            page.Content().PaddingBottom(20);
                        }

                        page.Content().PaddingBottom(30);
                    }
                });
            });

            return document.GeneratePdf();
        }
    }
}
