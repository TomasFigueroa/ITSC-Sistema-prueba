using ClosedXML.Excel;
using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace INTITUTO1.Server.Controllers
{
    [Route("api/Reporte")]
    [ApiController]
    public class ControllerReporte : ControllerBase
    {
        private readonly Context _context;

        public ControllerReporte(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PorCelda")]
        public IActionResult ExportExcel1()
        {

            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.AddWorksheet("Sample Sheet");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";

                using var memoria = new MemoryStream();
                workbook.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;

            }
            catch (Exception)
            {
                throw;

            }
        }

        [HttpGet]
        [Route("PorColumna")]
        public IActionResult ExportExcel2()
        {
            try
            {
                DataTable table = new DataTable();//tabla general

                table.Columns.Add("NAME");
                table.Columns.Add("ADRESS");
                table.Columns.Add("DATE");
                table.Columns.Add("INDICE");

                //Aqui van las filas y puedo añadir las filas que quiera o traerlas de una base de datos y con foreach interar

                DataRow fila = table.NewRow();
                fila["NAME"] = "Juan";
                fila["ADRESS"] = "Stret";
                fila["DATE"] = "23/12/2023";
                fila["INDICE"] = "Ok";


                table.Rows.Add(fila);


                using var libro = new XLWorkbook();
                table.TableName = "Registros";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();
                //agregar tablas de tanques al excel

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            }
            catch (Exception)
            {
                throw;

            }
        }


        [HttpPost("Template")]
        public IActionResult ExportExcel3([FromBody] CertificadoExamen certificado)
        {
            try
            {

                using (var workbook = new XLWorkbook(@"C:\Users\Usuario\source\repos\ITSC-Sistema-prueba2\INTITUTO1\Client\wwwroot\CERTIFICADO.xlsx"))
                {
                    var SampleSheet = workbook.Worksheets.First(x => x.Name == "Certificado");

                    SampleSheet.Cell("I10").Value = certificado.NombreAdministrador;
                    SampleSheet.Cell("Z12").Value = certificado.DniAlumno;
                    SampleSheet.Cell("H12").Value = certificado.NombreAlumno;
                    //SampleSheet.Cell("I16").Value = certificado.Carrera;
                    SampleSheet.Cell("V20").Value = certificado.Interesado;
                    SampleSheet.Cell("L24").Value = certificado.DiaNumero;
                    SampleSheet.Cell("X24").Value = certificado.Mes;
                    SampleSheet.Cell("AH24").Value = certificado.Anio;

                    using var memoria = new MemoryStream();
                    workbook.SaveAs(memoria);
                    var nombreExcel = "CertificadoExamen.xlsx";
                    var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                    return archivo;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el archivo: {ex.Message}");
            }
        }
        [HttpPost("ExportExcel/{idAlumno}")]
        public async Task<IActionResult> ExportExcel(int idAlumno)
        {
            var query = from nota in _context.notas
                        join divCicMatAlum in _context.DivsionCiclosMateriaAlumnos on nota.DivsionCiclosMateriaAlumnosIdDivCicMatAlum equals divCicMatAlum.IdDivCicMatAlum
                        join alumno in _context.alumnos on divCicMatAlum.AlumnosIdAlumno equals alumno.IdAlumno
                        join divCicMat in _context.DivisionCicloMaterias on divCicMatAlum.DivisionCicloMateriaIdDivCicMat equals divCicMat.IdDivCicMat
                        join divisionCiclo in _context.DivisionCiclos on divCicMat.DivisionCicloIdDivCic equals divisionCiclo.IdDivCic
                        join ciclo in _context.Ciclos on divisionCiclo.CicloIdCiclo equals ciclo.IdCiclo
                        join materia in _context.Materia on divCicMat.MateriasIdMateria equals materia.IdMateria
                        join tipoEvaluacion in _context.TipoEvaluacions on nota.TipoEvaluacionIdTipoEva equals tipoEvaluacion.IdTipoEva
                        where alumno.IdAlumno == idAlumno // Filtrar por el ID del alumno específico
                        select new NotasDto
                        {
                            Id = nota.IdNotas,
                            AlumnoNombre = alumno.Nombre,
                            AlumnoApellido = alumno.Apellido,
                            AlumnoDni = alumno.DNI_Alum,
                            AlumnoCuil = alumno.Cuil,
                            Materia = materia.Nombre,
                            Fecha = nota.Fecha,
                            Nota = nota.Nota,
                            TipoEvaluacion = tipoEvaluacion.NombreEva
                        };

            var notasAlumno = await query.ToListAsync();

            if (!notasAlumno.Any())
            {
                return NotFound($"No se encontraron notas para el alumno con ID {idAlumno}.");
            }

            try
            {
                // Cargar el archivo de plantilla
                using (var workbook = new XLWorkbook(@"C:\Users\Usuario\source\repos\ITSC-Sistema-prueba2\INTITUTO1\Client\wwwroot\Notas.xlsx"))
                {
                    var sheet = workbook.Worksheet(1); // Usa la primera hoja de la plantilla

                    // Encabezado del Alumno en las celdas de K19 a L22
                    var alumnoData = notasAlumno.First();
                    sheet.Cell("A19").Value = "Apellido:";
                    sheet.Cell("B19").Value = alumnoData.AlumnoApellido;
                    sheet.Cell("A20").Value = "Nombre:";
                    sheet.Cell("B20").Value = alumnoData.AlumnoNombre;
                    sheet.Cell("A21").Value = "DNI:";
                    sheet.Cell("B21").Value = alumnoData.AlumnoDni;
                    sheet.Cell("A22").Value = "CUIL:";
                    sheet.Cell("B22").Value = alumnoData.AlumnoCuil;

                    // Encabezados de la tabla de notas desde la fila K24
                    sheet.Cell("A24").Value = "Materia";
                    sheet.Cell("B24").Value = "Fecha";
                    sheet.Cell("C24").Value = "Nota";
                    sheet.Cell("D24").Value = "Tipo de Evaluación";

                    // Insertar datos de notas comenzando desde la fila 25 en adelante
                    int currentRow = 25;
                    foreach (var nota in notasAlumno)
                    {
                        sheet.Cell($"A{currentRow}").Value = nota.Materia;
                        sheet.Cell($"B{currentRow}").Value = nota.Fecha.ToShortDateString();
                        sheet.Cell($"C{currentRow}").Value = nota.Nota;
                        sheet.Cell($"D{currentRow}").Value = nota.TipoEvaluacion;
                        currentRow++;
                    }

                    // Guardar el archivo en un MemoryStream
                    using var memoryStream = new MemoryStream();
                    workbook.SaveAs(memoryStream);
                    var nombreExcel = "ReporteNotasAlumno.xlsx";
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el archivo: {ex.Message}");
            }
        }



    }
}
