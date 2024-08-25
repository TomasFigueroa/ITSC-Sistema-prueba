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

            var alumnos = await _excelService.CargarDatosDesdeExcel(file.OpenReadStream());

            if (alumnos == null || alumnos.Count == 0)
                return NotFound("No se encontraron datos en el archivo.");

            return Ok(alumnos);
        }
    }
}

