using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Divisiones
    {
        public int IdDivision { get; set; }
        public string NombreDiv { get; set; }
        public string NombreCarrera { get; set; }

        //Clave foranea
        public int CarrerassIdCarrera { get; set; }
        public Carrerass Carrerass { get; set; }

        public int MateriasIdMateria { get; set; }
        public Materias materias { get; set; }
        //public List<Materias> materias { get; set; }
        public List<DivisionCiclo> divisionCiclos { get; set; }

    }
}
