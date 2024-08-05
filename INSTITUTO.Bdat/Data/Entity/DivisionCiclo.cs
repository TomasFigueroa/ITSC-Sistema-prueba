using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class DivisionCiclo
    {
        public int IdDivCic { get; set; }
        //Foranea De Ciclo
        public int CicloIdCiclo { get; set; }
        public Ciclos Ciclo { get; set; }
        //Foranea de Division
        public int DivisionesIdDivision { get; set; }
        public Divisiones Divisiones { get; set; }

        public List<DivisionCicloMateria> DivisionCicloMateria { get; set; }
        //public ICollection<DivisionCicloMateria> DivisionCicloMaterias { get; set; } 
    }
}
