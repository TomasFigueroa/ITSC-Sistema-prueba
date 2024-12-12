using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTOMaterias
    {
        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una carrera.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una carrera válida.")]
        public int IdCarrera { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una división.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una división válida.")]
        public int IdDivision { get; set; }
    }
}
