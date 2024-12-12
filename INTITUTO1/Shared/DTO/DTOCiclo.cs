using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTOCiclo
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public String Fecha { get; set; } 
    }
}
