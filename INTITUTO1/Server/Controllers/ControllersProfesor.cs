using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;






namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Profesor")]
    public class ControllersProfesor : ControllerBase
    {
        private readonly Context _context;

        public ControllersProfesor(Context context)
        {
            _context = context;
        }

        // GET: api/Profesor
        [HttpGet]
        public async Task<ActionResult<List<Profesor>>> Get()
        {
            return await _context.profesors.ToListAsync();
        }

        // GET: api/Profesor/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profesor>> Get(int id)
        {
            var profesor = await _context.profesors.FirstOrDefaultAsync(c => c.IdProfesor == id);

            if (profesor == null)
            {
                return BadRequest($"No se encontró la nota con id: {id}");
            }

            return profesor;
        }

        // POST: api/Profesor
        [HttpPost]
        public async Task<ActionResult<Profesor>> Post(DTOProfesor dtoProfesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseApi = new ResponseAPI<int>();
            var mdProfesor = new Profesor
            {
                Apellido_Prof = dtoProfesor.Apellido_Prof,
                Nombre_Prof = dtoProfesor.Nombre_Prof,
                Dni = dtoProfesor.Dni,
                Estado = dtoProfesor.Estado,
            };
            _context.profesors.Add(mdProfesor);
            await _context.SaveChangesAsync();
            responseApi.Valor = mdProfesor.IdProfesor;
            responseApi.EsCorrecto = true;
            return Ok(responseApi);

        }



        // PUT: api/Profesor/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOProfesor dtoProfesor)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProfesor = await _context.profesors.FirstOrDefaultAsync(e => e.IdProfesor == id);

                if (dbProfesor != null)
                {

                    dbProfesor.Apellido_Prof = dtoProfesor.Apellido_Prof;
                    dbProfesor.Nombre_Prof = dtoProfesor.Nombre_Prof;
                    dbProfesor.Dni = dtoProfesor.Dni;
                    dbProfesor.Estado = dtoProfesor.Estado;


                    _context.profesors.Update(dbProfesor);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Profesor no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException.Message;
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

                var dbProfesor = await _context.profesors.FirstOrDefaultAsync(e => e.IdProfesor == id);

                if (dbProfesor != null)
                {
                    _context.profesors.Remove(dbProfesor);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Profesor no encontrada";
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
