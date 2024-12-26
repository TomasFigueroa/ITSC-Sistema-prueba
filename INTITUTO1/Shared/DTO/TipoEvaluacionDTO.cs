using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class TipoEvaluacionDTO
    {
        public int IdTipoEva { get; set; }

        [Required(ErrorMessage = "El nombre de la evaluación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreEva { get; set; }
    }

}
