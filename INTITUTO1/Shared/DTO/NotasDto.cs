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
        public string Materia { get; set; }
        public DateTime Fecha { get; set; }
        public int Nota { get; set; }
        public string TipoEvaluacion { get; set; }
    }

}
