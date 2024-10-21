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

    
        public int IdCarrera { get; set; }

        //Conexion por detras para no generar un ciclo
        public int IdDivision { get; set; }
        //Genera Ciclos
        //public Divisiones divisiones { get; set; }

        public List<DivisionCicloMateria> DivisionCicloMateria { get; set; }
       

    }
}
