using INSTITUTO.Bdat.Data.Entity;
using INSTITUTO.Bdat;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTITUTO1.Server.Controllers
{
	[ApiController]
	[Route("api/DivisionCiclo")]
	public class ControllersDivisionCiclo : ControllerBase
	{
		private readonly Context _context;

		public ControllersDivisionCiclo(Context context)
		{
			_context = context;
		}

		// GET: api/DivisionCiclo
		[HttpGet]
		public async Task<ActionResult<List<DivisionCiclo>>> Get()
		{
			return await _context.DivisionCiclos.ToListAsync();
		}

		// GET: api/DivsionCiclo/{id}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<DivisionCiclo>> Get(int id)
		{
			var DivCiclo = await _context.DivisionCiclos.FirstOrDefaultAsync(c => c.IdDivCic == id);

			if (DivCiclo == null)
			{
				return BadRequest($"No se encontró la DivisionCiclo con id: {id}");
			}

			return DivCiclo;
		}

        // POST: api/Carrera
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<int>>> Post(DTODivisionCiclo dtoDivisionCiclo)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                if (dtoDivisionCiclo == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El DTO de la División-Ciclo es nulo.";
                    return BadRequest(responseApi);
                }

                // Validar si ya existe un registro con la misma combinación de CicloIdCiclo y DivisionesIdDivision
                var divisionCicloExistente = await _context.DivisionCiclos
                    .FirstOrDefaultAsync(dc => dc.CicloIdCiclo == dtoDivisionCiclo.CicloIdCiclo
                                            && dc.DivisionesIdDivision == dtoDivisionCiclo.DivisionesIdDivision);

                if (divisionCicloExistente != null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ya existe una combinación de Ciclo y División con esos valores.";
                    return BadRequest(responseApi);
                }

                // Crear un nuevo registro si no existe una combinación igual
                var mdDivCic = new DivisionCiclo
                {
                    CicloIdCiclo = dtoDivisionCiclo.CicloIdCiclo,
                    DivisionesIdDivision = dtoDivisionCiclo.DivisionesIdDivision
                };

                _context.DivisionCiclos.Add(mdDivCic);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = mdDivCic.IdDivCic; // Devolver el ID de la nueva División-Ciclo
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }




        // PUT: api/DivisionCiclo/{id}
        [HttpPut("{id:int}")]
		public async Task<IActionResult> Put(int id, DTODivisionCiclo dtoDivisionCiclo)
		{
			var responseApi = new ResponseAPI<int>();

			try
			{
				var dbDivCic = await _context.DivisionCiclos.FirstOrDefaultAsync(e => e.IdDivCic == id);

				if (dbDivCic!= null)
				{

					dbDivCic.CicloIdCiclo = dbDivCic.CicloIdCiclo;
					dbDivCic.DivisionesIdDivision = dbDivCic.DivisionesIdDivision;
					

					_context.DivisionCiclos.Update(dbDivCic);
					await _context.SaveChangesAsync();
					responseApi.EsCorrecto = true;

				}
				else
				{
					responseApi.EsCorrecto = false;
					responseApi.Mensaje = "DivisionCiclo no encontrada";
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

				var dbDivCic = await _context.DivisionCiclos.FirstOrDefaultAsync(e => e.IdDivCic == id);

				if (dbDivCic != null)
				{
					_context.DivisionCiclos.Remove(dbDivCic);
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
