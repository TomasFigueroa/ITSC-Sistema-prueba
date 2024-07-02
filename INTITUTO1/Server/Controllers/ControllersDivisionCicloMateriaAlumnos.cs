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
	public class ControllersDivisionCicloMateriaAlumnos
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

        // GET: api/DivsionCicloMateriaAlumnos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DivsionCiclosMateriaAlumnos>> Get(int id)
        {
            var carrera = await _context.DivsionCiclosMateriaAlumnos.FirstOrDefaultAsync(c => c.IdDivCicMatAlum == id);

            if (carrera == null)
            {
                return BadRequest($"No se encontró la DivisionCiclo con id: {id}");
            }

            return carrera;
        }

        // POST: api/CiclosMateriaAlumnos
        [HttpPost]
        public async Task<ActionResult<DivsionCiclosMateriaAlumnos>> Post(DTODivisionCicloMateriaAlumno dtoDivisionCicloMateriaAlumno)
        {
            var responseApi = new ResponseAPI<int>();
            var mdDivCicMatAlu = new DivsionCiclosMateriaAlumnos
            {
                DivisionCicloMateriaIdDivCicMat = dtoDivisionCicloMateriaAlumno.DivisionCicloMateriaIdDivCicMat,
                AlumnosIdAlumno = dtoDivisionCicloMateriaAlumno.AlumnosIdAlumno,
                LibrosId_Libro = dtoDivisionCicloMateriaAlumno.LibrosId_Libro,

            };
            _context.DivsionCiclosMateriaAlumnos.Add(mdDivCicMatAlu);
            await _context.SaveChangesAsync();
            return Ok(responseApi);

        }



        // PUT: api/DivisionCiclosMateriaAlumnos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTODivisionCicloMateriaAlumno dtoDivisionCicloMateriaAlumno)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivCicMatAlu = await _context.DivsionCiclosMateriaAlumnos.FirstOrDefaultAsync(e => e.IdDivCicMatAlum == id);

                if (dbDivCicMatAlu != null)
                {

                    dbDivCicMatAlu.DivisionCicloMateriaIdDivCicMat = dbDivCicMatAlu.DivisionCicloMateriaIdDivCicMat;
                    dbDivCicMatAlu.AlumnosIdAlumno = dbDivCicMatAlu.AlumnosIdAlumno;
                    dbDivCicMatAlu.LibrosId_Libro = dbDivCicMatAlu.LibrosId_Libro;

                    _context.DivsionCiclosMateriaAlumnos.Update(dbDivCicMatAlu);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;

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
                responseApi.Mensaje = ex.InnerException.Message;
            }
            return Ok(responseApi);
        }

        // DELETE: api/DivisionCicloMateriaAlumno/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbDivCicMatAlu = await _context.DivsionCiclosMateriaAlumnos.FirstOrDefaultAsync(e => e.IdDivCicMatAlum == id);

                if (dbDivCicMatAlu != null)
                {
                    _context.DivsionCiclosMateriaAlumnos.Remove(dbDivCicMatAlu);
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
