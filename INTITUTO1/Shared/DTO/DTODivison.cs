using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTODivison
    {
        [Required(ErrorMessage = "El nombre de la división es obligatorio.")]
        public string NombreDiv { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una carrera.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una carrera válida.")]
        public int NombreCar { get; set; }
    }
}
