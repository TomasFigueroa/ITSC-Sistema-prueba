using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class DivsionCiclosMateriaAlumnos
    {
        public int IdDivCicMatAlum { get; set; }

        public int DivisionCicloMateriaIdDivCicMat { get; set; }
        public DivisionCicloMateria DivisionCicloMateria { get; set; }

        public int AlumnosIdAlumno { get; set; }
        public Alumnos Alumnos { get; set; }

        public int LibrosId_Libro { get; set; }
        public LIbros LIbros { get; set; }

        public List<Notas> Notas { get; set; }

    }
}
