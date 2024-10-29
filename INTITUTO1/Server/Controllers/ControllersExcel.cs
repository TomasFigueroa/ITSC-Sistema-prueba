using INTITUTO1.Server.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace INTITUTO1.Server.Controllers
{
    [Route("api/CargarExcel")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ExcelService _excelService;

        public ExcelController(ExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost("Cargar")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Debe seleccionar un archivo.");

            try
            {
                // Cargar los alumnos desde el archivo Excel
                var alumnos = await _excelService.CargarDatosDesdeExcel(file.OpenReadStream());

                if (alumnos == null || alumnos.Count == 0)
                    return NotFound("No se encontraron datos en el archivo.");

                // Validar duplicados (DNI en la misma carrera)
                var alumnoCarreraSet = new HashSet<string>(); // Para almacenar DNI + carrera
                foreach (var alumno in alumnos)
                {
                    string key = $"{alumno.DNI_Alum}_{alumno.Id_Carrera}"; // clave combinada de DNI y carrera

                    if (!alumnoCarreraSet.Add(key))
                    {
                        // Si no se pudo agregar, es porque ya existe un duplicado
                        return Conflict($"El alumno con DNI {alumno.DNI_Alum} ya está registrado en la carrera {alumno.Id_Carrera}.");
                    }
                }

                // Si no hay duplicados, retornar los alumnos cargados exitosamente
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                return StatusCode(500, "Ocurrió un error al procesar el archivo.");
            }
        }

    }
}

