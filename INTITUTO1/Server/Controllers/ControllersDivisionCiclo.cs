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
			var carrera = await _context.DivisionCiclos.FirstOrDefaultAsync(c => c.IdDivCic == id);

			if (carrera == null)
			{
				return BadRequest($"No se encontró la DivisionCiclo con id: {id}");
			}

			return carrera;
		}

		// POST: api/Carrera
		[HttpPost]
		public async Task<ActionResult<DivisionCiclo>> Post(DTODivisionCiclo dtoDivisionCiclo)
		{
			var responseApi = new ResponseAPI<int>();
			var mdDivCic = new DivisionCiclo
			{
				CicloIdCiclo = dtoDivisionCiclo.CicloIdCiclo,
				DivisionesIdDivision = dtoDivisionCiclo.DivisionesIdDivision,

			};
			_context.DivisionCiclos.Add(mdDivCic);
			await _context.SaveChangesAsync();
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
