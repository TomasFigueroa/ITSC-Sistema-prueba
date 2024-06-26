using INSTITUTO.Bdat.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTITUTO1.Shared.DTO;
using INSTITUTO.Bdat;


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
        public async Task<ActionResult<Carrerass>> Post(DTOSCarreras carrera)
        {
            var responseApi = new ResponseAPI<int>();
            var mdCarrera = new Carrerass
            {
                Nombre = carrera.Nombres,
                FechaInicio = carrera.Fecha_inicio,
                FechaFin = carrera.Fecha_fin,
               
            };
            _context.Carreras.Add(mdCarrera);
            await _context.SaveChangesAsync();
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

        // DELETE: api/Carrera/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                //var dbReserva = await context.Reservas.FirstOrDefaultAsync(e => e.NroReserva == nroRes);
                var dbCarrera = await _context.Carreras.FirstOrDefaultAsync(e => e.IdCarrera == id);

                if (dbCarrera != null)
                {
                    _context.Carreras.Remove(dbCarrera);
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

    }
}
