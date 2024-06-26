using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTOSCarreras
    {

        public String Nombres { get; set; }
        [Required(ErrorMessage = "La Fecha de inicio es Obligatoria")]
        public DateTime Fecha_inicio { get; set; }
        [Required(ErrorMessage = "La Fecha de fin es Obligatoria")]
        public DateTime Fecha_fin { get; set; }
       
    }
}
