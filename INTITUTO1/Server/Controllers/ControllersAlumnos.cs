
using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Alumnos")]
    public class ControllersAlumnos : ControllerBase
    {
        private readonly Context _context;

        public ControllersAlumnos(Context context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public async Task<ActionResult<List<Alumnos>>> get()
        {
            return await _context.alumnos.ToListAsync();
        }

        //GET: api/Alumnos/{id}
        [HttpGet("{id:int}")]

        public async Task<ActionResult<Alumnos>> Get(int id)
        {
            var alumno = await _context.alumnos.FirstOrDefaultAsync(c => c.IdAlumno == id);

            if (alumno == null)
            {
                return BadRequest($"No se encontro el alumno con el id: {id}");

            }
            return alumno;
        }

        [HttpPost("crear-alumnos")]
        public async Task<ActionResult<ResponseAPI<List<int>>>> Post(List<DTOAlumnos> dtoAlumnos)
        {
            var responseApi = new ResponseAPI<List<int>>();
            var idsAlumnos = new List<int>();

            try
            {
                if (dtoAlumnos == null || dtoAlumnos.Count == 0)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "La lista de DTOs de alumnos está vacía o es nula.";
                    return BadRequest(responseApi);
                }

                foreach (var dtoAlumno in dtoAlumnos)
                {
                    var mdAlumno = new Alumnos
                    {
                        Nombre = dtoAlumno.Nombre,
                        Apellido = dtoAlumno.Apellido,
                        DNI_Alum = dtoAlumno.DNI_Alum,
                        Cuil = dtoAlumno.Cuil,
                        Fecha_Nac = dtoAlumno.Fecha_Nac,
                        Tbase = dtoAlumno.Tbase,
                        Nacionalidad = dtoAlumno.Nacionalidad,
                        Estado = dtoAlumno.Estado,
                        Numero = dtoAlumno.Numero,
                        Sexo =dtoAlumno.Sexo,
                        Id_Carrera = dtoAlumno.Id_Carrera
                    };

                    _context.alumnos.Add(mdAlumno);
                    await _context.SaveChangesAsync();
                    idsAlumnos.Add(mdAlumno.IdAlumno);
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = idsAlumnos; // Devolver los ids de los alumnos creados
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        // POST api/Alumnos
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTOAlumnos dtoAlumno)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                if (dtoAlumno == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El DTO del alumno es nulo.";
                    return BadRequest(responseApi);
                }

                // Validar si ya existe un alumno con el mismo DNI y CUIL en la misma carrera
                var alumnoExistente = await _context.alumnos
                    .FirstOrDefaultAsync(a => a.DNI_Alum == dtoAlumno.DNI_Alum
                                           && a.Cuil == dtoAlumno.Cuil
                                           && a.Id_Carrera == dtoAlumno.Id_Carrera);

                if (alumnoExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe un alumno con el mismo DNI y CUIL en esta carrera.";
                    return BadRequest(responseApi);
                }

                var mdAlumno = new Alumnos
                {
                    Nombre = dtoAlumno.Nombre,
                    Apellido = dtoAlumno.Apellido,
                    DNI_Alum = dtoAlumno.DNI_Alum,
                    Cuil = dtoAlumno.Cuil,
                    Fecha_Nac = dtoAlumno.Fecha_Nac,
                    Tbase = dtoAlumno.Tbase,
                    Nacionalidad = dtoAlumno.Nacionalidad,
                    Estado = dtoAlumno.Estado,
                    Numero = dtoAlumno.Numero,
                    Sexo = dtoAlumno.Sexo,
                    Id_Carrera = dtoAlumno.Id_Carrera
                };

                _context.alumnos.Add(mdAlumno);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = mdAlumno.IdAlumno; // Devolver el id del alumno creado
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        // PUT: api/Alumnos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTOAlumnos dtoAlumnos)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbAlumno = await _context.alumnos.FirstOrDefaultAsync(e => e.IdAlumno == id);

                if (dbAlumno != null)
                {
                    dbAlumno.Nombre = dtoAlumnos.Nombre;
                    dbAlumno.Apellido = dtoAlumnos.Apellido;
                    dbAlumno.DNI_Alum = dtoAlumnos.DNI_Alum;
                    dbAlumno.Cuil = dtoAlumnos.Cuil;
                    dbAlumno.Fecha_Nac = dtoAlumnos.Fecha_Nac;
                    dbAlumno.Tbase = dtoAlumnos.Tbase;
                    dbAlumno.Nacionalidad = dtoAlumnos.Nacionalidad;
                    dbAlumno.Estado = dtoAlumnos.Estado;
                    dbAlumno.Numero = dtoAlumnos.Numero;
                    dbAlumno.Sexo = dtoAlumnos.Sexo;
                    dbAlumno.Id_Carrera = dtoAlumnos.Id_Carrera;

                    _context.alumnos.Update(dbAlumno);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Alumno no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }


        //DELETE: api/Alumnos/{id}
        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbAlumno = await _context.alumnos.FirstOrDefaultAsync(e => e.IdAlumno == id);

                if (dbAlumno != null)
                {
                    _context.alumnos.Remove(dbAlumno);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Alumno no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto= false;
                responseApi.Mensaje = ex.InnerException.Message;
            }
            return Ok (responseApi);

        }

    }
}
