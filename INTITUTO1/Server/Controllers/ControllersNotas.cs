using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Notas")]
    public class ControllersNotas : ControllerBase
    {
        private readonly Context _context;

        public ControllersNotas(Context context)
        {
            _context = context;
        }

        // GET: api/Notas
        [HttpGet]
        public async Task<ActionResult<List<Notas>>> Get()
        {
            return await _context.notas.ToListAsync();
        }
      
        // GET: api/Notas/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Notas>> Get(int id)
        {
            var nota = await _context.notas.FirstOrDefaultAsync(c => c.IdNotas == id);

            if (nota == null)
            {
                return NotFound($"No se encontró la nota con id: {id}");
            }

            return nota;
        }

        // POST: api/Notas
        [HttpPost]
        public async Task<ActionResult<Notas>> Post(DTONotas dtoNotas)
        {
            var responseApi = new ResponseAPI<int>();
            var mdNota = new Notas
            {
                Nota = dtoNotas.Nota,
                Fecha = dtoNotas.Fecha,
                DivsionCiclosMateriaAlumnosIdDivCicMatAlum = dtoNotas.Materias,
                TipoEvaluacionIdTipoEva = dtoNotas.TipoEvaluacionIdTipoEva,
                //TipoEvaluacion = dtoNotas.TipoEvaluacion (desmarcala si va esta)
            };
            _context.notas.Add(mdNota);
            await _context.SaveChangesAsync();
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Nota creada correctamente";
            return Ok(responseApi);
        }

        // PUT: api/Notas/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTONotas dtoNotas)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbNotas = await _context.notas.FirstOrDefaultAsync(e => e.IdNotas == id);

                if (dbNotas != null)
                {
                    dbNotas.Nota = dtoNotas.Nota;
                    dbNotas.Fecha = dtoNotas.Fecha;
                    dbNotas.DivsionCiclosMateriaAlumnosIdDivCicMatAlum = dtoNotas.Materias;
                    dbNotas.TipoEvaluacionIdTipoEva = dtoNotas.TipoEvaluacionIdTipoEva;

                    _context.notas.Update(dbNotas);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Nota actualizada correctamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Nota no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }

        // DELETE: api/Notas/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbNotas = await _context.notas.FirstOrDefaultAsync(e => e.IdNotas == id);

                if (dbNotas != null)
                {
                    _context.notas.Remove(dbNotas);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Nota eliminada correctamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Nota no encontrada";
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
