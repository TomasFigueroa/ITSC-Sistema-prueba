using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class LIbros
    {
        public int Id_Libro { get; set; }
        public string Nombre_Lib { get; set; }

        public List<Notas> notas { get; set; }
    }
}
