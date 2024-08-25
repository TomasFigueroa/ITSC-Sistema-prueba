using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace INTITUTO1.Server.Servicios

{
    public class ExcelService
    {
        public async Task<List<AlumnoDto>> CargarDatosDesdeExcel(Stream fileStream)
        {
            var alumnos = new List<AlumnoDto>();

            using (var workbook = new XLWorkbook(fileStream))
            {
                //lee la primera hoja
                var worksheet = workbook.Worksheet(1);
                var rowCount = worksheet.LastRowUsed().RowNumber();

                //comenzar desde la fila 2 para omitir encabezados
                for (int row = 2; row <= rowCount; row++)
                {
                    var alumno = new AlumnoDto
                    {
                        Nombre = worksheet.Cell(row, 1).GetString(),
                        Apellido = worksheet.Cell(row, 2).GetString(),
                        DNI_Alum = worksheet.Cell(row, 3).GetValue<int>(),
                        Cuil = worksheet.Cell(row, 4).GetString(),
                        Fecha_Nac = worksheet.Cell(row, 5).GetDateTime(),
                        Tbase = worksheet.Cell(row, 6).GetString(),
                        Nacionalidad = worksheet.Cell(row, 7).GetString(),
                        Estado = worksheet.Cell(row, 8).GetBoolean()
                    };
                    alumnos.Add(alumno);
                }
            }

            return alumnos;
        }
    }

    public class AlumnoDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI_Alum { get; set; }
        public string Cuil { get; set; }
        public DateTime Fecha_Nac { get; set; }
        public string Tbase { get; set; }
        public string Nacionalidad { get; set; }
        public bool Estado { get; set; }
    }
}
