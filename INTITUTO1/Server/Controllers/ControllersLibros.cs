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

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<List<LIbros>>> Get()
        {
            return await _context.LIbros.ToListAsync();
        }

        // GET: api/Libros/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LIbros>> Get(int id)
        {
            var libro = await _context.LIbros.FindAsync(id);

            if (libro == null)
            {
                return NotFound($"No se encontró el libro con id: {id}");
            }

            return libro;
        }

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTOLibros dtoLibros)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Validar si ya existe un libro con el mismo nombre
                var libroExistente = await _context.LIbros
                    .FirstOrDefaultAsync(l => l.Nombre_Lib.ToLower() == dtoLibros.Nombre_Lib.ToLower());

                if (libroExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe un libro con el mismo nombre.";
                    return BadRequest(responseApi);
                }

                // Crear un nuevo libro si no existe uno con el mismo nombre
                var nuevoLibro = new LIbros
                {
                    Nombre_Lib = dtoLibros.Nombre_Lib
                };
                _context.LIbros.Add(nuevoLibro);
                await _context.SaveChangesAsync();

                responseApi.Valor = nuevoLibro.Id_Libro;
                responseApi.EsCorrecto = true;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return BadRequest(responseApi);
            }
        }


        // PUT: api/Libros/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOLibros dtoLibros)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {   
                var libroExistente = await _context.LIbros.FindAsync(id);

                if (libroExistente == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Libro no encontrado";
                    return NotFound(responseApi);
                }

                libroExistente.Nombre_Lib = dtoLibros.Nombre_Lib;

                _context.LIbros.Update(libroExistente);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return BadRequest(responseApi);
            }
        }

        // DELETE: api/Libros/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var libroExistente = await _context.LIbros.FindAsync(id);

                if (libroExistente == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Libro no encontrado";
                    return NotFound(responseApi);
                }

                _context.LIbros.Remove(libroExistente);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return BadRequest(responseApi);
            }
        }
    }
}
