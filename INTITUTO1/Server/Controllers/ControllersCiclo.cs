
using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace INTITUTO1.Server.Controllers
{
    [Route("api/Ciclo")]
    [ApiController]
    public class CicloController : ControllerBase
    {
        private readonly Context _context;

        public CicloController(Context context)
        {
            _context = context;
        }

        // GET: api/Ciclo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciclos>>> get()
        {
            return await _context.Ciclos.ToListAsync();
        }

        // GET: api/Ciclo/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ciclos>> GetCiclo(int id)
        {
            var ciclo = await _context.Ciclos.FirstOrDefaultAsync(c => c.IdCiclo == id);

            if (ciclo == null)
            {

                return BadRequest($"No se encontro el ciclo con el id: {id}");
            }

            return ciclo;
        }

        // POST: api/Ciclo
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTOCiclo dtoCiclo)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                if (dtoCiclo == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El DTO del ciclo es nulo.";
                    return BadRequest(responseApi);
                }

                // Validar si ya existe un ciclo con la misma fecha
                var cicloExistente = await _context.Ciclos
                    .FirstOrDefaultAsync(c => c.Fecha == dtoCiclo.Fecha);

                if (cicloExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe un ciclo con la misma fecha.";
                    return BadRequest(responseApi);
                }

                // Crear un nuevo ciclo si no existe uno con la misma fecha
                var mdCiclo = new Ciclos
                {
                    Fecha = dtoCiclo.Fecha
                };

                _context.Ciclos.Add(mdCiclo);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = mdCiclo.IdCiclo; // Devolver el id del ciclo creado
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        //// PUT: api/Ciclo
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOCiclo dtoCiclo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbCiclo = await _context.Ciclos.FirstOrDefaultAsync(e => e.IdCiclo == id);

                if (dbCiclo == null)
                {
                    dbCiclo.Fecha = dtoCiclo.Fecha;

                    _context.Ciclos.Update(dbCiclo);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
            }
            catch (Exception ex) 
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException.Message;


            }
            return Ok(responseApi);
        }

        // DELETE: api/Ciclo/5
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var responseApi = new ResponseAPI<int>();

                try
                {
                    var dbCiclo = await _context.Ciclos.FirstOrDefaultAsync(e => e.IdCiclo == id);

                    if (dbCiclo != null)
                    {
                        _context.Ciclos.Remove(dbCiclo);
                        await _context.SaveChangesAsync();
                        responseApi.EsCorrecto = true;
                    }
                    else
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "Ciclo no encontrado";
                    }
                }
                catch (Exception ex)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = ex.InnerException.Message;
                }
                return Ok(responseApi);

            }
        
    } }
