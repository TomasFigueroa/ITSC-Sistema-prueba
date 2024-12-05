using INSTITUTO.Bdat.Data.Entity;
using INSTITUTO.Bdat;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/DivisionCicloMateriaAlumnos")]
    public class ControllersDivisionCicloMateriaAlumnos : ControllerBase
    {
        private readonly Context _context;

        public ControllersDivisionCicloMateriaAlumnos(Context context)
        {
            _context = context;
        }

        // GET: api/DivisionCicloMateriaAlumnos
        [HttpGet]
        public async Task<ActionResult<List<DivsionCiclosMateriaAlumnos>>> Get()
        {
            return await _context.DivsionCiclosMateriaAlumnos.ToListAsync();
        }

        // GET: api/DivisionCicloMateriaAlumnos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DivsionCiclosMateriaAlumnos>> Get(int id)
        {
            var divisionCicloMateriaAlumno = await _context.DivsionCiclosMateriaAlumnos
                .Include(d => d.Alumnos) // Incluye la entidad Alumnos relacionada
                .FirstOrDefaultAsync(c => c.IdDivCicMatAlum == id);

            if (divisionCicloMateriaAlumno == null)
            {
                return NotFound($"No se encontró la DivisionCicloMateriaAlumno con id: {id}");
            }

            return Ok(new
            {
                divisionCicloMateriaAlumno.IdDivCicMatAlum,
                divisionCicloMateriaAlumno.DivisionCicloMateriaIdDivCicMat,
                AlumnosIdAlumno = divisionCicloMateriaAlumno.Alumnos.IdAlumno,
                NombreAlumno = divisionCicloMateriaAlumno.Alumnos.Nombre,  //aca hehe
           
            });
        }

        // POST: api/DivisionCicloMateriaAlumnos
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTODivisionCicloMateriaAlumno dtoDivisionCicloMateriaAlumno)
        {
            var responseApi = new ResponseAPI<int>();

            // Verificar si el alumno ya está registrado en la misma materia
            var existeAlumnoEnMateria = await _context.DivsionCiclosMateriaAlumnos
                .AnyAsync(x => x.DivisionCicloMateriaIdDivCicMat == dtoDivisionCicloMateriaAlumno.DivisionCicloMateriaIdDivCicMat
                            && x.AlumnosIdAlumno == dtoDivisionCicloMateriaAlumno.AlumnosIdAlumno);

            if (existeAlumnoEnMateria)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "El alumno ya está registrado en esta materia.";
                return BadRequest(responseApi);
            }

            // Crear nuevo registro si no existe
            var newDivCicMatAlu = new DivsionCiclosMateriaAlumnos
            {
                DivisionCicloMateriaIdDivCicMat = dtoDivisionCicloMateriaAlumno.DivisionCicloMateriaIdDivCicMat,
                AlumnosIdAlumno = dtoDivisionCicloMateriaAlumno.AlumnosIdAlumno,
            };

            _context.DivsionCiclosMateriaAlumnos.Add(newDivCicMatAlu);
            await _context.SaveChangesAsync();

            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Registro creado con éxito";
            responseApi.Valor = newDivCicMatAlu.IdDivCicMatAlum;

            return Ok(responseApi);
        }


        // PUT: api/DivisionCicloMateriaAlumnos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseAPI<int>>> Put(int id, DTODivisionCicloMateriaAlumno dtoDivisionCicloMateriaAlumno)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivCicMatAlu = await _context.DivsionCiclosMateriaAlumnos.FirstOrDefaultAsync(e => e.IdDivCicMatAlum == id);

                if (dbDivCicMatAlu != null)
                {
                    dbDivCicMatAlu.DivisionCicloMateriaIdDivCicMat = dtoDivisionCicloMateriaAlumno.DivisionCicloMateriaIdDivCicMat;
                    dbDivCicMatAlu.AlumnosIdAlumno = dtoDivisionCicloMateriaAlumno.AlumnosIdAlumno;
                   

                    _context.DivsionCiclosMateriaAlumnos.Update(dbDivCicMatAlu);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Registro actualizado con éxito";
                    responseApi.Valor = dbDivCicMatAlu.IdDivCicMatAlum;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "DivisionCicloMateriaAlumno no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        // DELETE: api/DivisionCicloMateriaAlumnos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseAPI<int>>> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivCicMatAlu = await _context.DivsionCiclosMateriaAlumnos
                    .Include(d => d.Notas)
                    .FirstOrDefaultAsync(e => e.IdDivCicMatAlum == id);

                if (dbDivCicMatAlu == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "División no encontrada.";
                    return NotFound(responseApi);
                }

                if (dbDivCicMatAlu.Notas != null && dbDivCicMatAlu.Notas.Any())
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se puede eliminar la división porque tiene notas asociadas.";
                    return Conflict(responseApi); // Devuelve un código 409 Conflict
                }

                _context.DivsionCiclosMateriaAlumnos.Remove(dbDivCicMatAlu);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Mensaje = "División eliminada con éxito.";
                responseApi.Valor = id;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi);
            }
        }


    }

}
