using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INTITUTO1.Shared.Validaciones;
using System;


namespace INTITUTO1.Shared.DTO
{
    
    public class DTOSCarreras
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de inicio debe ser válida.")]
        public DateTime Fecha_inicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de fin debe ser válida.")]
        [DateGreaterThan("Fecha_inicio", ErrorMessage = "La fecha de fin debe ser posterior a la fecha de inicio.")]
        public DateTime Fecha_fin { get; set; }
    }

}
