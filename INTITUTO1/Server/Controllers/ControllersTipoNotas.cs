using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace INTITUTO1.Server.Controllers
{
    

    [Route("api/TipoNota")]
    [ApiController]
    public class TipoEvaluacionController : ControllerBase
    {
        private readonly Context _context;

        public TipoEvaluacionController(Context context)
        {
            _context = context;
        }

        // GET: api/TipoEvaluacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEvaluacionDTO>>> GetTipoEvaluaciones()
        {
            var tipoEvaluaciones = await _context.TipoEvaluacions
                .Select(te => new TipoEvaluacionDTO
                {
                    IdTipoEva = te.IdTipoEva,
                    NombreEva = te.NombreEva
                })
                .ToListAsync();

            return Ok(tipoEvaluaciones);
        }

        // GET: api/TipoEvaluacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEvaluacionDTO>> GetTipoEvaluacion(int id)
        {
            var tipoEvaluacion = await _context.TipoEvaluacions
                .Select(te => new TipoEvaluacionDTO
                {
                    IdTipoEva = te.IdTipoEva,
                    NombreEva = te.NombreEva
                })
                .FirstOrDefaultAsync(te => te.IdTipoEva == id);

            if (tipoEvaluacion == null)
            {
                return NotFound();
            }

            return Ok(tipoEvaluacion);
        }

        // POST: api/TipoEvaluacion
        [HttpPost]
        public async Task<ActionResult<TipoEvaluacionDTO>> PostTipoEvaluacion(TipoEvaluacionDTO tipoEvaluacionDto)
        {
            // Verificar si ya existe un tipo de evaluación con el mismo nombre
            var tipoEvaluacionExistente = await _context.TipoEvaluacions
                .FirstOrDefaultAsync(te => te.NombreEva == tipoEvaluacionDto.NombreEva);

            if (tipoEvaluacionExistente != null)
            {
                // Retornar un error de conflicto si ya existe
                return Conflict(new { message = "Ya existe un tipo de evaluación con este nombre." });
            }

            // Si no existe, proceder a crear el nuevo tipo de evaluación
            var tipoEvaluacion = new TipoEvaluacion
            {
                NombreEva = tipoEvaluacionDto.NombreEva
            };

            _context.TipoEvaluacions.Add(tipoEvaluacion);
            await _context.SaveChangesAsync();

            tipoEvaluacionDto.IdTipoEva = tipoEvaluacion.IdTipoEva;

            return CreatedAtAction(nameof(GetTipoEvaluacion), new { id = tipoEvaluacion.IdTipoEva }, tipoEvaluacionDto);
        }


        // PUT: api/TipoEvaluacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoEvaluacion(int id, TipoEvaluacionDTO tipoEvaluacionDto)
        {
            if (id != tipoEvaluacionDto.IdTipoEva)
            {
                return BadRequest();
            }

            var tipoEvaluacion = await _context.TipoEvaluacions.FindAsync(id);
            if (tipoEvaluacion == null)
            {
                return NotFound();
            }

            tipoEvaluacion.NombreEva = tipoEvaluacionDto.NombreEva;

            _context.Entry(tipoEvaluacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEvaluacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TipoEvaluacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoEvaluacion(int id)
        {
            var tipoEvaluacion = await _context.TipoEvaluacions.FindAsync(id);
            if (tipoEvaluacion == null)
            {
                return NotFound();
            }

            _context.TipoEvaluacions.Remove(tipoEvaluacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoEvaluacionExists(int id)
        {
            return _context.TipoEvaluacions.Any(e => e.IdTipoEva == id);
        }
    }

}
