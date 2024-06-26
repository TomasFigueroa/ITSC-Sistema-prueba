using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Profesor
    {
        public int IdProfesor { get; set; }
        public string Apellido_Prof { get; set; }
        public string Nombre_Prof { get; set; }
        public int Dni { get; set; }
        public bool Estado { get; set; }

        //public ICollection<DivisionCicloMateria> DivisionCicloMaterias { get; set; }



    }
}
