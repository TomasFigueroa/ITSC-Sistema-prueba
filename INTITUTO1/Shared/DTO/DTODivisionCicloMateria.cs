using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTODivisionCicloMateria
    {
        [Required(ErrorMessage = "Debe seleccionar una División-Ciclo.")]
        public int DivisionCicloIdDivCic { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Materia.")]
        public int MateriasIdMateria { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Profesor.")]
        public int ProfesorIdProfesor { get; set; }
    }
}
