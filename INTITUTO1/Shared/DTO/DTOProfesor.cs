using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTOProfesor
    {
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido_Prof { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre_Prof { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI debe ser valido.")]
        public int Dni { get; set; }

        public bool Estado { get; set; }
    }
}
