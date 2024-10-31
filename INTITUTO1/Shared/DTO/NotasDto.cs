using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class NotasDto
    {
        public int Id { get; set; }
        public string AlumnoNombre { get; set; }
        public string AlumnoApellido { get; set; }
        public string AlumnoDni { get; set; }
        public string AlumnoCuil { get; set; }
        public string Materia { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Ciclo { get; set; }
        public int Nota { get; set; }
        public string Carrera { get; set; }
        public string TipoEvaluacion { get; set; }
        public string Division { get; set; }
    }

}
