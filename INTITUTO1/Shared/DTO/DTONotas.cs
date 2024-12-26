using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTONotas
    {
        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(1, 10, ErrorMessage = "La nota debe estar entre 1 y 10.")]
        public int Nota { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una materia.")]
        [Range(1, int.MaxValue, ErrorMessage = "Materia inválida.")]
        public int Materias { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de evaluación.")]
        [Range(1, int.MaxValue, ErrorMessage = "Evaluación inválida.")]
        public int TipoEvaluacionIdTipoEva { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Libro inválido.")]
        public int? Idlibro { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de folio debe ser mayor a 0.")]
        public int? NumeroFolio { get; set; }
    }
}
