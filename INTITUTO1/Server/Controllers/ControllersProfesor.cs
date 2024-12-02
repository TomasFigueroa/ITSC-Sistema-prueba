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
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTOProfesor dtoProfesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseApi = new ResponseAPI<int>();

            // Validar si ya existe un profesor con el mismo DNI
            var profesorExistente = await _context.profesors
                .FirstOrDefaultAsync(p => p.Dni == dtoProfesor.Dni);

            if (profesorExistente != null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Ya existe un profesor con el mismo DNI.";
                return BadRequest(responseApi);
            }

            // Crear nuevo profesor si no existe uno con el mismo DNI
            var mdProfesor = new Profesor
            {
                Apellido_Prof = dtoProfesor.Apellido_Prof,
                Nombre_Prof = dtoProfesor.Nombre_Prof,
                Dni = dtoProfesor.Dni,
                Estado = true,
            };

            _context.profesors.Add(mdProfesor);
            await _context.SaveChangesAsync();

            responseApi.Valor = mdProfesor.IdProfesor;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Profesor creado con éxito.";
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
                // Buscar al profesor en la base de datos
                var dbProfesor = await _context.profesors.FirstOrDefaultAsync(e => e.IdProfesor == id);

                if (dbProfesor != null)
                {
                    // Verificar si existe relación con DivisionCicloMateria
                    var tieneRelaciones = await _context.DivisionCicloMaterias
                        .AnyAsync(d => d.ProfesorIdProfesor == id);

                    if (tieneRelaciones)
                    {
                        // Si tiene relaciones, cambiar el estado a falso
                        dbProfesor.Estado = false; 
                        _context.profesors.Update(dbProfesor);
                    }
                    else
                    {
                        // Si no tiene relaciones, eliminar el registro
                        _context.profesors.Remove(dbProfesor);
                    }

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = tieneRelaciones
                        ? "Profesor inactivado debido a relaciones existentes."
                        : "Profesor eliminado correctamente.";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Profesor no encontrado.";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> GetDeletedItems()
        {
            var eliminados = await _context.profesors
                .Where(e => !e.Estado) // Suponiendo que "Estado" determina si está activo
                .Select(e => new Profesor
                {
                    Nombre_Prof = e.Nombre_Prof,
                    Dni = e.Dni
                })
                .ToListAsync();

            return Ok(eliminados);
        }

        [HttpPost("activar/{id}")]
        public async Task<IActionResult> ActivateItem(int id)
        {
            var item = await _context.profesors.FirstOrDefaultAsync(e => e.IdProfesor == id);
            if (item == null)
            {
                return NotFound(new { mensaje = "Elemento no encontrado." });
            }

            item.Estado = true; // Cambiar estado a activo
            _context.profesors.Update(item);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Elemento activado correctamente." });
        }

    }
}
