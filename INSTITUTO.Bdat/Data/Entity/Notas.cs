using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Notas
    {
        public int IdNotas { get; set; }
        public int Nota { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoEvaluacionIdTipoEva { get; set; }
        public TipoEvaluacion TipoEvaluacion { get; set; }
        public int LibrosId_Libro { get; set; }
        public LIbros LIbros { get; set; }
        public int DivsionCiclosMateriaAlumnosIdDivCicMatAlum { get; set; }
        public DivsionCiclosMateriaAlumnos DivsionCiclosMateriaAlumnos { get; set; }
    }
}
