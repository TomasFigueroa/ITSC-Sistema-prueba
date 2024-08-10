using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using static INTITUTO1.Client.Pages.Reporte;


namespace INTITUTO1.Server.Servicios
{
    public class PdfDocumentService
    {
        public byte[] GeneratePdf(List<Alumnos> alumnos)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1, Unit.Inch);
                    page.Header().Text("Reporte de Alumnos").Bold().FontSize(20);

                    foreach (var alumno in alumnos)
                    {
                        page.Content().Text($"Nombre: {alumno.Nombre} {alumno.Apellido}");
                        page.Content().Text($"DNI: {alumno.DNI_Alum}");
                        page.Content().Text($"Fecha de Nacimiento: {alumno.Fecha_Nac:dd/MM/yyyy}");
                        page.Content().Text($"Estado: {(alumno.Estado ? "Activo" : "Inactivo")}");
                        page.Content().PaddingBottom(10);

                        //recorrer la lista de DivsionCiclosMateriaAlumnos del alumno
                        foreach (var divCicMatAlum in alumno.DivsionCiclosMateriaAlumnos)
                        {
                            //agregar tabla de notas
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

                                // Recorrer la lista de Notas para cada DivsionCiclosMateriaAlumnos
                                foreach (var nota in divCicMatAlum.Notas)
                                {
                                    //table.Cell().Text(nota.TipoEvaluacion.Nombre); REVISAR
                                    table.Cell().Text(nota.Nota.ToString());
                                    table.Cell().Text(nota.Fecha.ToString("dd/MM/yyyy"));
                                }
                            });

                            //espacio entre secciones de notas
                            page.Content().PaddingBottom(20); 
                        }
                        //Espacio entre alumnos
                        page.Content().PaddingBottom(30); 
                    }
                });
            });

            return document.GeneratePdf();
        }
    }
}

