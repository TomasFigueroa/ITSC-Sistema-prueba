using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Materias
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }

        
        
        public List<Divisiones> Divisiones { get; set; }

        //public List<DivisionCicloMateria> DivisionCicloMateria { get; set; }
    }
}
