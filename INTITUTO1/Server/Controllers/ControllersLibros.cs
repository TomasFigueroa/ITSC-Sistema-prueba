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
                    Nombre_Lib = dtoLibros.Nombre_Lib,
                    Estado = true
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
                libroExistente.Estado = true;

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
                // Buscar el libro en la base de datos
                var libroExistente = await _context.LIbros
                                                   .Include(l => l.notas) // Incluir las relaciones con Notas
                                                   .FirstOrDefaultAsync(l => l.Id_Libro == id);

                if (libroExistente != null)
                {
                    // Verificar si el libro tiene relaciones con Notas
                    var tieneRelacion = libroExistente.notas != null && libroExistente.notas.Any();

                    if (tieneRelacion)
                    {
                        // Si tiene relaciones, cambiar el estado del libro a inactivo
                        libroExistente.Estado = false;
                        _context.LIbros.Update(libroExistente);
                        responseApi.Mensaje = "El libro tiene notas asociadas. Su estado ha sido cambiado a inactivo.";
                    }
                    else
                    {
                        // Si no tiene relaciones, eliminar el libro
                        _context.LIbros.Remove(libroExistente);
                        responseApi.Mensaje = "Libro eliminado correctamente.";
                    }

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Libro no encontrado.";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }

            return Ok(responseApi);
        }




        // PUT: api/Libros/activar/{id}
        [HttpPut("activar/{id}")]
        public async Task<IActionResult> ActivateLibro(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Buscar el libro
                var libroExistente = await _context.LIbros.FindAsync(id);

                if (libroExistente == null || libroExistente.Estado)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Libro no encontrado o ya está activo.";
                    return NotFound(responseApi);
                }

                // Cambiar el estado del libro a true
                libroExistente.Estado = true;

                // Actualizar el registro
                _context.LIbros.Update(libroExistente);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Mensaje = "Libro activado con éxito.";
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return BadRequest(responseApi);
            }

            return Ok(responseApi);
        }
    }
}
