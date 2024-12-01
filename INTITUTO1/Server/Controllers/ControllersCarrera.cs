using INSTITUTO.Bdat.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTITUTO1.Shared.DTO;
using INSTITUTO.Bdat;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Carrera")]
    public class ControllersCarrera : ControllerBase
    {
        private readonly Context _context;

        public ControllersCarrera(Context context)
        {
            _context = context;
        }

        // GET: api/Carrera
        [HttpGet]
        public async Task<ActionResult<List<Carrerass>>> Get()
        {
            return await _context.Carreras.ToListAsync();
        }

        // GET: api/Carrera/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Carrerass>> Get(int id)
        {
            var carrera = await _context.Carreras.FirstOrDefaultAsync(c => c.IdCarrera == id);

            if (carrera == null)
            {
                return BadRequest($"No se encontró la carrera con id: {id}");
            }

            return carrera;
        }

        // POST: api/Carrera
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTOSCarreras carrera)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                if (carrera == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El DTO de la carrera es nulo.";
                    return BadRequest(responseApi);
                }

                // Validar si ya existe una carrera con el mismo nombre
                var carreraExistente = await _context.Carreras
                    .FirstOrDefaultAsync(c => c.Nombre.ToLower() == carrera.Nombres.ToLower());

                if (carreraExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe una carrera con el mismo nombre.";
                    return BadRequest(responseApi);
                }

                var mdCarrera = new Carrerass
                {
                    Nombre = carrera.Nombres,
                    FechaInicio = carrera.Fecha_inicio,
                    FechaFin = carrera.Fecha_fin,
                    Estado = true
                 
                };

                _context.Carreras.Add(mdCarrera);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = mdCarrera.IdCarrera; // Devolver el id de la carrera creada
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }




        // PUT: api/Carrera/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOSCarreras carrera)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbCarrera = await _context.Carreras.FirstOrDefaultAsync(e => e.IdCarrera == id);

                if (dbCarrera != null)
                {

                    dbCarrera.Nombre = carrera.Nombres;
                    dbCarrera.FechaInicio = carrera.Fecha_inicio;
                    dbCarrera.FechaFin = carrera.Fecha_fin;
                    dbCarrera.Estado = true;

                    _context.Carreras.Update(dbCarrera);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Carrera no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut("activar/{id}")]
        public async Task<IActionResult> ActivarCarrera(int id)
        {
            var carrera = await _context.Carreras.FirstOrDefaultAsync(c => c.IdCarrera == id);

            if (carrera == null)
            {
                return NotFound(new { mensaje = "Carrera no encontrada." });
            }

            carrera.Estado = true;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Carrera activada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor.", detalle = ex.Message });
            }
        }


        // DELETE: api/Carrera/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Verificar si la carrera existe
                var carrera = await _context.Carreras.FirstOrDefaultAsync(e => e.IdCarrera == id);

                if (carrera == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Carrera no encontrada.";
                    return NotFound(responseApi);
                }

                // Verificar si tiene divisiones relacionadas
                var tieneDivisionesRelacionadas = await _context.Division.AnyAsync(d => d.CarrerassIdCarrera == id);

                if (tieneDivisionesRelacionadas)
                {
                    // Cambiar el estado a inactivo
                    carrera.Estado = false; // Suponiendo que 'Estado' es un booleano
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "El estado de la carrera ha sido cambiado a inactivo debido a divisiones relacionadas.";
                }
                else
                {
                    // Eliminar la carrera directamente si no tiene relaciones
                    _context.Carreras.Remove(carrera);
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Carrera eliminada correctamente.";
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
