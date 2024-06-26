using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Ciclo
    {
        public int IdCiclo { get; set; }
        public DateTime Fecha { get; set; }

        public List<DivisionCiclo> divisionCiclos { get; set; }
    }
}
