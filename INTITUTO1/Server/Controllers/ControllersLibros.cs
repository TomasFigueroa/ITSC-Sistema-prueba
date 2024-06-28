using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Libros")]
    public class ControllersLibros : ControllerBase
    {
        private readonly Context _context;

        public ControllersLibros(Context context)
        {
            _context = context;
        }

        // GET: api/Carrera
        [HttpGet]
        public async Task<ActionResult<List<LIbros>>> Get()
        {
            return await _context.LIbros.ToListAsync();
        }

        // GET: api/libros/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LIbros>> Get(int id)
        {
            var libro = await _context.LIbros.FirstOrDefaultAsync(c => c.Id_Libro == id);

            if (libro == null)
            {
                return BadRequest($"No se encontró la carrera con id: {id}");
            }

            return libro;
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<LIbros>> Post(DTOLibros dtoLibros)
        {
            var responseApi = new ResponseAPI<int>();
            var mdLibro = new LIbros
            {
                Nombre_Lib = dtoLibros.Nombre_Lib
    

            };
            _context.LIbros.Add(mdLibro);
            await _context.SaveChangesAsync();
            return Ok(responseApi);

        }



        // PUT: api/libros/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOLibros dtoLibros)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbLibros = await _context.LIbros.FirstOrDefaultAsync(e => e.Id_Libro == id);

                if (dbLibros != null)
                {

                    dbLibros.Nombre_Lib = dtoLibros.Nombre_Lib;


                    _context.LIbros.Update(dbLibros);
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

                var dbLibros = await _context.LIbros.FirstOrDefaultAsync(e => e.Id_Libro == id);

                if (dbLibros != null)
                {
                    _context.LIbros.Remove(dbLibros);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Libro no encontrada";
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
