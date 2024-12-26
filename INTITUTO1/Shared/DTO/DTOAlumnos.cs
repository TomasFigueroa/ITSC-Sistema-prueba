using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTOAlumnos
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression("\\d{8}", ErrorMessage = "El DNI debe tener 8 dígitos.")]
        public string DNI_Alum { get; set; }

        [Required(ErrorMessage = "El CUIL es obligatorio.")]
        [RegularExpression("\\d{2}\\d{8}\\d{1}", ErrorMessage = "El CUIL debe tener 11 dígitos.")]
        public string Cuil { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime Fecha_Nac { get; set; }

        [Required(ErrorMessage = "El tipo de base es obligatorio.")]
        public string Tbase { get; set; }

        [Required(ErrorMessage = "La nacionalidad es obligatoria.")]
        public string Nacionalidad { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "El número es obligatorio.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        public int Id_Carrera { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio.")]
        public string Sexo { get; set; }
    }
}
