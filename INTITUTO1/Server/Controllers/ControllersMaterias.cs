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
            return await _context.Materia.ToListAsync();
        }

        // GET: api/Materias/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Materias>> Get(int id)
        {
            var materias = await _context.Materia.FirstOrDefaultAsync(c => c.IdMateria == id);

            if (materias == null)
            {
                return BadRequest($"No se encontró la materia con id: {id}");
            }

            return materias;
        }

        // POST: api/materias
        [HttpPost]
        public async Task<ActionResult<LIbros>> Post(DTOMaterias dtoMaterias)
        {
            var responseApi = new ResponseAPI<int>();
            var mdMateria = new Materias
            {
                
                Nombre = dtoMaterias.Nombre

            };
            _context.Materia.Add(mdMateria);
            await _context.SaveChangesAsync();
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


                    _context.Materia.Update(dbMaterias);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;

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
                responseApi.Mensaje = ex.InnerException.Message;
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
                responseApi.Mensaje = ex.InnerException.Message;
            }
            return Ok(responseApi);
        }

    }
}
