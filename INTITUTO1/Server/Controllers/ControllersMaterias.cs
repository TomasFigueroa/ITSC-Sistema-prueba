using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Materias")]
    public class ControllersMaterias : ControllerBase
    {
        private readonly Context _context;

        public ControllersMaterias(Context context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<List<Materias>>> Get()
        {
            return await _context.Materia.Include(m => m.DivisionCicloMateria).ToListAsync();
        }

        // GET: api/Materias/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Materias>> Get(int id)
        {
            var materias = await _context.Materia.Include(m => m.DivisionCicloMateria)
                                                 .FirstOrDefaultAsync(c => c.IdMateria == id);

            if (materias == null)
            {
                return BadRequest($"No se encontró la materia con id: {id}");
            }

            return materias;
        }

        // POST: api/materias
        [HttpPost]
        public async Task<ActionResult<Materias>> Post(DTOMaterias dtoMaterias)
        {
            var responseApi = new ResponseAPI<int>();
            var mdMateria = new Materias
            {
                Nombre = dtoMaterias.Nombre,
                IdCarrera = dtoMaterias.IdCarrera,
                IdDivision = dtoMaterias.IdDivision,
                
            };
            _context.Materia.Add(mdMateria);
            await _context.SaveChangesAsync();
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Materia creada con éxito";
            return Ok(responseApi);
        }

        // PUT: api/materias/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOMaterias dtoMaterias)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbMaterias = await _context.Materia.FirstOrDefaultAsync(e => e.IdMateria == id);

                if (dbMaterias != null)
                {
                    dbMaterias.Nombre = dtoMaterias.Nombre;
                    dbMaterias.IdCarrera = dtoMaterias.IdCarrera;
                    dbMaterias.IdDivision = dtoMaterias.IdDivision;
                    

                    _context.Materia.Update(dbMaterias);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Materia actualizada con éxito";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Materia no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }

        // DELETE: api/Materias/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbMaterias = await _context.Materia.FirstOrDefaultAsync(e => e.IdMateria == id);

                if (dbMaterias != null)
                {
                    _context.Materia.Remove(dbMaterias);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Materia eliminada con éxito";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Materia no encontrada";
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
