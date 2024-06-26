using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class DivisionCicloMateria
    {
        public int IdDivCicMat { get; set; }
        //Foranea
        public int DivisionCicloIdDivCic { get; set; }
        public DivisionCiclo DivisionCiclo { get; set; }
        //Foranea
        public int MateriasIdMateria { get; set; }
        public Materias Materias { get; set; }
        //Foranea
        public int ProfesorIdProfesor { get; set; }
        public Profesor Profesor { get; set; }
        public List<DivsionCiclosMateriaAlumnos> DivsionCiclosMateriaAlumnos { get; set; }

    }
}
