using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTODivisionCiclo
    {
        [Required(ErrorMessage = "Debe seleccionar un ciclo.")]
        public int CicloIdCiclo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una división.")]
        public int DivisionesIdDivision { get; set; }
    }
}
