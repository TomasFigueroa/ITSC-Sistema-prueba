using INSTITUTO.Bdat.Data.Entity;
using INSTITUTO.Bdat;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/DivisionCicloMateria")]
    public class ControllersDivisionCicloMateria : ControllerBase
    {
        private readonly Context _context;

        public ControllersDivisionCicloMateria(Context context)
        {
            _context = context;
        }

        // GET: api/DivisionCicloMateria
        [HttpGet]
        public async Task<ActionResult<List<DivisionCicloMateria>>> Get()
        {
            return await _context.DivisionCicloMaterias.ToListAsync();
        }
       
        // GET: api/DivisionCicloMateria/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DivisionCicloMateria>> Get(int id)
        {
            var DivCicloMat = await _context.DivisionCicloMaterias.FirstOrDefaultAsync(c => c.IdDivCicMat == id);

            if (DivCicloMat == null)
            {
                return BadRequest($"No se encontró la DivisionCiclo con id: {id}");
            }

            return DivCicloMat;
        }

        // POST: api/DivisionCicloMateria
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTODivisionCicloMateria dtoDivisionCicloMateria)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                if (dtoDivisionCicloMateria == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El DTO de Division-Ciclo-Materia es nulo.";
                    return BadRequest(responseApi);
                }

                // Validar si ya existe un registro con la misma combinación de DivisionCicloIdDivCic, MateriasIdMateria y ProfesorIdProfesor
                var divisionCicloMateriaExistente = await _context.DivisionCicloMaterias
                    .FirstOrDefaultAsync(dcm => dcm.DivisionCicloIdDivCic == dtoDivisionCicloMateria.DivisionCicloIdDivCic
                                             && dcm.MateriasIdMateria == dtoDivisionCicloMateria.MateriasIdMateria
                                             && dcm.ProfesorIdProfesor == dtoDivisionCicloMateria.ProfesorIdProfesor);

                if (divisionCicloMateriaExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe una combinación de División-Ciclo, Materia y Profesor con esos valores.";
                    return BadRequest(responseApi);
                }

                // Crear un nuevo registro si no existe una combinación igual
                var mdDivCicMat = new DivisionCicloMateria
                {
                    DivisionCicloIdDivCic = dtoDivisionCicloMateria.DivisionCicloIdDivCic,
                    MateriasIdMateria = dtoDivisionCicloMateria.MateriasIdMateria,
                    ProfesorIdProfesor = dtoDivisionCicloMateria.ProfesorIdProfesor
                };

                _context.DivisionCicloMaterias.Add(mdDivCicMat);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = mdDivCicMat.IdDivCicMat; // Devolver el ID de la nueva División-Ciclo-Materia
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }




        // PUT: api/DivisionCicloMateria/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTODivisionCicloMateria dtoDivisionCicloMateria)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbDivCicMat = await _context.DivisionCicloMaterias.FirstOrDefaultAsync(e => e.IdDivCicMat == id);

                if (dbDivCicMat != null)
                {
                    // Actualiza las propiedades con los valores del DTO
                    dbDivCicMat.DivisionCicloIdDivCic = dtoDivisionCicloMateria.DivisionCicloIdDivCic;
                    dbDivCicMat.MateriasIdMateria = dtoDivisionCicloMateria.MateriasIdMateria;
                    dbDivCicMat.ProfesorIdProfesor = dtoDivisionCicloMateria.ProfesorIdProfesor;

                    _context.DivisionCicloMaterias.Update(dbDivCicMat);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "DivisionCicloMateria no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }


        // DELETE: api/DivisionCicloMateria/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbDivCicMat = await _context.DivisionCicloMaterias.FirstOrDefaultAsync(e => e.IdDivCicMat == id);

                if (dbDivCicMat != null)
                {
                    _context.DivisionCicloMaterias.Remove(dbDivCicMat);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "DivisionCicloMateria no encontrada";
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
