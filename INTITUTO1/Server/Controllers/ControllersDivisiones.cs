using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Division")]
    public class ControllersDivision : ControllerBase
    {
        private readonly Context _context;

        public ControllersDivision(Context context)
        {
            _context = context;
        }

        // GET: api/Division
        [HttpGet]
        public async Task<ActionResult<List<Divisiones>>> Get()
        {
            return await _context.Division.ToListAsync();
        }

        [HttpGet("Inactivas")]
        public async Task<IActionResult> GetInactivas()
        {
            var inactivas = await _context.Division
                .Where(d => d.Estado == false) // Asume que 'Estado' indica si está activa o no
                .ToListAsync();
            return Ok(inactivas);
        }


        // GET: api/Division/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Divisiones>> Get(int id)
        {
            var division = await _context.Division.FirstOrDefaultAsync(d => d.IdDivision == id);

            if (division == null)
            {
                return BadRequest($"No se encontró la división con id: {id}");
            }

            return division;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTODivison dtoDivision)
        {
            var responseApi = new ResponseAPI<int>();

            // Buscar el IdCarrera correspondiente al NombreCar
            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(c => c.IdCarrera == dtoDivision.NombreCar);

            if (carrera == null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "La carrera especificada no existe.";
                return NotFound(responseApi);
            }

            var division = new Divisiones
            {
                NombreDiv = dtoDivision.NombreDiv,
                CarrerassIdCarrera = carrera.IdCarrera, // Usar el IdCarrera de la carrera encontrada
                Estado = true,

            };

            _context.Division.Add(division);
            await _context.SaveChangesAsync();

            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "División creada exitosamente.";
            /*responseApi.Data = division.IdDivision;*/ // Devolver el ID de la nueva división creada
            return Ok(responseApi);
        }



        // PUT: api/Division/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTODivison dtoDivision)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivision = await _context.Division.FirstOrDefaultAsync(e => e.IdDivision == id);

                if (dbDivision != null)
                {
                    dbDivision.NombreDiv = dtoDivision.NombreDiv;
                    dbDivision.CarrerassIdCarrera = dtoDivision.NombreCar;
                    dbDivision.Estado = true;

                    _context.Division.Update(dbDivision);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "División no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException.Message;
            }
            return Ok(responseApi);
        }
        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> ActivateDivision(int id)
        {
            var division = await _context.Division.FindAsync(id);
            if (division == null)
                return NotFound();

            division.Estado = true; // Cambiar estado a activo
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/Division/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Buscar la división en la base de datos
                var dbDivision = await _context.Division.FirstOrDefaultAsync(e => e.IdDivision == id);

                if (dbDivision == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "División no encontrada.";
                    return NotFound(responseApi);
                }

                // Verificar si existen materias relacionadas con esta división
                bool hasRelatedMaterias = await _context.Materia.AnyAsync(m => m.IdDivision == id);

                if (hasRelatedMaterias)
                {
                    // Cambiar el estado de la división a inactivo (false)
                    dbDivision.Estado = false; // Suponiendo que 'Estado' es un booleano
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "El estado de la división ha sido cambiado a inactivo debido a materias asociadas.";
                }
                else
                {
                    // Eliminar la división directamente si no tiene relaciones
                    _context.Division.Remove(dbDivision);
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "División eliminada correctamente.";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }

            return Ok(responseApi);
        }

    }

}
