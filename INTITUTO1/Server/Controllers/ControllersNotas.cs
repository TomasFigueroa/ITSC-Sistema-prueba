﻿using INSTITUTO.Bdat;
using INSTITUTO.Bdat.Data.Entity;
using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Notas")]
    public class ControllersNotas : ControllerBase
    {
        private readonly Context _context;

        public ControllersNotas(Context context)
        {
            _context = context;
        }

        // GET: api/Notas
        [HttpGet("api/Notas")]
        public async Task<ActionResult<List<Notas>>> Get()
        {
            return await _context.notas.ToListAsync();
        }

        [HttpGet("GetNotas")]
        public async Task<IActionResult> GetNotas()
        {
            var query = from nota in _context.notas
                        join divCicMatAlum in _context.DivsionCiclosMateriaAlumnos on nota.DivsionCiclosMateriaAlumnosIdDivCicMatAlum equals divCicMatAlum.IdDivCicMatAlum
                        join alumno in _context.alumnos on divCicMatAlum.AlumnosIdAlumno equals alumno.IdAlumno
                        join divCicMat in _context.DivisionCicloMaterias on divCicMatAlum.DivisionCicloMateriaIdDivCicMat equals divCicMat.IdDivCicMat
                        join divisionCiclo in _context.DivisionCiclos on divCicMat.DivisionCicloIdDivCic equals divisionCiclo.IdDivCic
                        join ciclo in _context.Ciclos on divisionCiclo.CicloIdCiclo equals ciclo.IdCiclo
                        join materia in _context.Materia on divCicMat.MateriasIdMateria equals materia.IdMateria
                        join tipoEvaluacion in _context.TipoEvaluacions on nota.TipoEvaluacionIdTipoEva equals tipoEvaluacion.IdTipoEva
                        select new NotasDto
                        {
                            Id = nota.IdNotas,
                            AlumnoNombre = alumno.Nombre,
                            Materia = materia.Nombre,
                            Fecha = nota.Fecha,
                            Nota = nota.Nota,
                            TipoEvaluacion = tipoEvaluacion.NombreEva
                        };

            return Ok(await query.ToListAsync());
        }



        // GET: api/Notas/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var nota = await (from n in _context.notas
                              join divCicMatAlum in _context.DivsionCiclosMateriaAlumnos
                              on n.DivsionCiclosMateriaAlumnosIdDivCicMatAlum equals divCicMatAlum.IdDivCicMatAlum

                              join alumno in _context.alumnos
                              on divCicMatAlum.AlumnosIdAlumno equals alumno.IdAlumno

                              join divCicMat in _context.DivisionCicloMaterias
                              on divCicMatAlum.DivisionCicloMateriaIdDivCicMat equals divCicMat.IdDivCicMat

                              join divisionCiclo in _context.DivisionCiclos
                              on divCicMat.DivisionCicloIdDivCic equals divisionCiclo.IdDivCic

                              join ciclo in _context.Ciclos
                              on divisionCiclo.CicloIdCiclo equals ciclo.IdCiclo

                              join materia in _context.Materia
                              on divCicMat.MateriasIdMateria equals materia.IdMateria

                              where n.IdNotas == id

                              select new
                              {
                                  AlumnoNombre = alumno.Nombre,
                                  AlumnoApellido = alumno.Apellido,
                                  Materia = materia.Nombre,
                                  Ciclo = ciclo.Fecha,
                                  Nota = n.Nota
                              }).FirstOrDefaultAsync();

            if (nota == null)
            {
                return NotFound($"No se encontró la nota con id: {id}");
            }

            return Ok(nota);
        }

        #region post viejo
        // POST: api/Notas
        //[HttpPost]
        //public async Task<IActionResult> Post(DTONotas dtoNotas)
        //{
        //    // Valida que el Materias exista en la tabla DivisionCicloMaterias
        //    var divisionCicloMateria = await _context.DivsionCiclosMateriaAlumnos
        //        .FirstOrDefaultAsync(dcm => dcm.IdDivCicMatAlum == dtoNotas.Materias);

        //    // Si no existe, devolver un error 400 BadRequest
        //    if (divisionCicloMateria == null)
        //    {
        //        return BadRequest("El ID de DivisionCicloMateria no es válido.");
        //    }

        //    // Crear una nueva nota utilizando el DTO
        //    var nuevaNota = new Notas
        //    {
        //        Nota = dtoNotas.Nota,
        //        Fecha = dtoNotas.Fecha,
        //        DivsionCiclosMateriaAlumnosIdDivCicMatAlum = dtoNotas.Materias, // Ajusta esto si el nombre de la propiedad es diferente
        //        TipoEvaluacionIdTipoEva = dtoNotas.TipoEvaluacionIdTipoEva
        //    };

        //    // Agrega la entidad al contexto
        //    _context.notas.Add(nuevaNota);

        //    // Guarda los cambios
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return CreatedAtAction(nameof(Get), new { id = nuevaNota.IdNotas }, nuevaNota); // Devuelve el recurso creado
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Manejo del error específico de claves foráneas
        //        return StatusCode(500, "Ocurrió un error al guardar los datos: " + ex.InnerException?.Message);
        //    }
        //}
        #endregion
        [HttpPost]
        public async Task<IActionResult> Post(DTONotas dtoNotas)
        {
            //validaciones
            if (dtoNotas == null || dtoNotas.Materias == 0 || dtoNotas.Nota < 0)
            {
                return BadRequest("Datos inválidos en la solicitud.");
            }

            var divisionCicloMateria = await _context.DivsionCiclosMateriaAlumnos
                .FirstOrDefaultAsync(dcm => dcm.IdDivCicMatAlum == dtoNotas.Materias);

            if (divisionCicloMateria == null)
            {
                return BadRequest("El ID de DivisionCicloMateria no es válido.");
            }

            var nuevaNota = new Notas
            {
                Nota = dtoNotas.Nota,
                Fecha = dtoNotas.Fecha,
                DivsionCiclosMateriaAlumnosIdDivCicMatAlum = dtoNotas.Materias,
                TipoEvaluacionIdTipoEva = dtoNotas.TipoEvaluacionIdTipoEva
            };

            _context.notas.Add(nuevaNota);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = nuevaNota.IdNotas }, nuevaNota);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al guardar los datos: " + ex.InnerException?.Message);
            }
        }


        // PUT: api/Notas/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, DTONotas dtoNotas)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbNotas = await _context.notas.FirstOrDefaultAsync(e => e.IdNotas == id);

                if (dbNotas != null)
                {
                    dbNotas.Nota = dtoNotas.Nota;
                    dbNotas.Fecha = dtoNotas.Fecha;
                    dbNotas.DivsionCiclosMateriaAlumnosIdDivCicMatAlum = dtoNotas.Materias;
                    dbNotas.TipoEvaluacionIdTipoEva = dtoNotas.TipoEvaluacionIdTipoEva;

                    _context.notas.Update(dbNotas);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Nota actualizada correctamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Nota no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }

        // DELETE: api/Notas/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbNotas = await _context.notas.FirstOrDefaultAsync(e => e.IdNotas == id);

                if (dbNotas != null)
                {
                    _context.notas.Remove(dbNotas);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Nota eliminada correctamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Nota no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.InnerException?.Message ?? ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet("GenerarReporte/{alumnoId}")]
        public IActionResult GenerarReporte(int alumnoId)
        {
            // Consulta para obtener las notas del alumno
            var query = from nota in _context.notas
                        join divCicMatAlum in _context.DivsionCiclosMateriaAlumnos on nota.DivsionCiclosMateriaAlumnosIdDivCicMatAlum equals divCicMatAlum.IdDivCicMatAlum
                        join alumno in _context.alumnos on divCicMatAlum.AlumnosIdAlumno equals alumno.IdAlumno
                        join divCicMat in _context.DivisionCicloMaterias on divCicMatAlum.DivisionCicloMateriaIdDivCicMat equals divCicMat.IdDivCicMat
                        join divisionCiclo in _context.DivisionCiclos on divCicMat.DivisionCicloIdDivCic equals divisionCiclo.IdDivCic
                        join ciclo in _context.Ciclos on divisionCiclo.CicloIdCiclo equals ciclo.IdCiclo
                        join materia in _context.Materia on divCicMat.MateriasIdMateria equals materia.IdMateria
                        join tipoEvaluacion in _context.TipoEvaluacions on nota.TipoEvaluacionIdTipoEva equals tipoEvaluacion.IdTipoEva
                        where alumno.IdAlumno == alumnoId  // Filtrar por el alumno específico
                        select new NotasDto
                        {
                            Id = nota.IdNotas,
                            AlumnoNombre = alumno.Nombre + " " + alumno.Apellido,
                            Materia = materia.Nombre,
                            Fecha = nota.Fecha,
                            Nota = nota.Nota,
                            TipoEvaluacion = tipoEvaluacion.NombreEva
                        };

            // Obtener el resultado de la consulta
            var notasAlumno = query.ToList();

            if (notasAlumno == null || !notasAlumno.Any())
            {
                return NotFound("No se encontraron notas para el alumno con el ID especificado.");
            }

            // Creamos un documento PDF en memoria
            using (var ms = new MemoryStream())
            {
                // Inicializa el escritor de PDF
                var writer = new iText.Kernel.Pdf.PdfWriter(ms);
                var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);

                // Título del documento
                var titulo = new iText.Layout.Element.Paragraph("Reporte de Notas del Alumno")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20);
                document.Add(titulo);

                // Información del alumno
                var infoAlumno = new iText.Layout.Element.Paragraph($"Alumno: {notasAlumno.First().AlumnoNombre}\n" +
                    $"Número de Alumno: {alumnoId}\n")
                    .SetMarginTop(20);
                document.Add(infoAlumno);

                // Calificaciones
                var tituloCalificaciones = new iText.Layout.Element.Paragraph("Calificaciones")
                    .SetFontSize(16)
                    .SetBold()
                    .SetMarginTop(20);
                document.Add(tituloCalificaciones);

                // Tabla de calificaciones
                var table = new iText.Layout.Element.Table(4); // 4 columnas: Asignatura, Nota, Tipo Evaluación, Fecha de aprobación
                table.AddHeaderCell("Asignatura");
                table.AddHeaderCell("Nota");
                table.AddHeaderCell("Tipo Evaluación");
                table.AddHeaderCell("Fecha Aprobado");

                foreach (var nota in notasAlumno)
                {
                    table.AddCell(nota.Materia);
                    table.AddCell(nota.Nota.ToString());
                    table.AddCell(nota.TipoEvaluacion);
                    table.AddCell(nota.Fecha.ToString("dd/MM/yyyy"));
                }

                document.Add(table);

                // Cerrar el documento
                document.Close();

                // Enviar el PDF generado al cliente
                var pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "Reporte_Notas_Alumno.pdf");
            }
        }


    }
}
   
    


