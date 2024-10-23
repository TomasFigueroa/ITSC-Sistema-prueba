using Microsoft.AspNetCore.Mvc;
using INSTITUTO.Bdat.Data.Entity;
using INSTITUTO.Bdat;
using INTITUTO1.Shared.DTO;

namespace INTITUTO1.Server.Controllers
{
    [Route("api/CrearCertificadoEx")]
    [ApiController]
    public class ControllersPdf : ControllerBase
    {
        private readonly PdfService _pdfService;

        public ControllersPdf(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost]
        public IActionResult GenerarCertificado([FromBody] CertificadoExamenDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El DTO no puede ser nulo.");
            }

            // Conversión del DTO a la entidad
            var model = new CertificadoExamen
            {
                NombreAdministrador = dto.NombreAdministrador,
                NombreAlumno = dto.NombreAlumno,
                DniAlumno = dto.DniAlumno,
                Carrera = dto.Carrera,
                Interesado = dto.Interesado,
                DiaNumero = dto.DiaNumero,
                Mes = dto.Mes,
                Anio = dto.Anio
            };

            try
            {
                var pdfBytes = _pdfService.GenerateCertificado(model);
                return File(pdfBytes, "application/pdf", "constancia.pdf");
            }
            catch (Exception ex)
            {
                // Registro del error (puedes usar un logger)
                return StatusCode(500, $"Error generando el PDF: {ex.Message}");
            }
        }
    }
}
