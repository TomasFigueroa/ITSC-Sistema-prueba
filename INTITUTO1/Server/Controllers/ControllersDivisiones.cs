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

        // DELETE: api/Division/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivision = await _context.Division.FirstOrDefaultAsync(e => e.IdDivision == id);

                if (dbDivision != null)
                {
                    _context.Division.Remove(dbDivision);
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
    }

}
