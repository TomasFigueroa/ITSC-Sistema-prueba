using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using INSTITUTO.Bdat.Data.Entity;
namespace INTITUTO1.Server.Controllers
{
    [Route("api/Reporte")]
    [ApiController]
    public class ControllerReporte : ControllerBase
    {
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
                    SampleSheet.Cell("W20").Value = certificado.Interesado;
                    SampleSheet.Cell("L24").Value = certificado.DiaNumero;
                    SampleSheet.Cell("Y24").Value = certificado.Mes;
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
    }
}
