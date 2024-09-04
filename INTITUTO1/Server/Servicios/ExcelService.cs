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
                var worksheet = workbook.Worksheet(1);
                var rowCount = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var alumno = new AlumnoDto
                        {
                            Nombre = worksheet.Cell(row, 1).GetString(),
                            Apellido = worksheet.Cell(row, 2).GetString(),
                            DNI_Alum = ConvertToInt(worksheet.Cell(row, 3)),
                            Cuil = worksheet.Cell(row, 4).GetString(),
                            Fecha_Nac = ConvertToDateTime(worksheet.Cell(row, 5)),
                            Tbase = worksheet.Cell(row, 6).GetString(),
                            Nacionalidad = worksheet.Cell(row, 7).GetString(),
                            Estado = ConvertToBoolean(worksheet.Cell(row, 8))
                        };
                        alumnos.Add(alumno);
                    }
                    catch (Exception ex)
                    {
                        // Log the error and continue with the next row
                        Console.WriteLine($"Error processing row {row}: {ex.Message}");
                    }
                }
            }

            return alumnos;
        }

        private int ConvertToInt(IXLCell cell)
        {
            if (int.TryParse(cell.Value.ToString(), out int result))
            {
                return result;
            }
            throw new FormatException($"Unable to convert '{cell.Value}' to int.");
        }

        private DateTime ConvertToDateTime(IXLCell cell)
        {
            if (cell.Value.IsDateTime)
            {
                return cell.GetDateTime();
            }
            if (DateTime.TryParse(cell.Value.ToString(), out DateTime result))
            {
                return result;
            }
            throw new FormatException($"Unable to convert '{cell.Value}' to DateTime.");
        }

        private bool ConvertToBoolean(IXLCell cell)
        {
            if (cell.Value.IsBoolean)
            {
                return cell.GetBoolean();
            }
            var stringValue = cell.Value.ToString().ToLower();
            if (stringValue == "true" || stringValue == "1" || stringValue == "sí" || stringValue == "si" || stringValue == "s")
            {
                return true;
            }
            if (stringValue == "false" || stringValue == "0" || stringValue == "no" || stringValue == "n")
            {
                return false;
            }
            throw new FormatException($"Unable to convert '{cell.Value}' to boolean.");
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

