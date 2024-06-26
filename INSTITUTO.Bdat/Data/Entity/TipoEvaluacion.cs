using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class TipoEvaluacion
    {
        public int IdTipoEva { get; set; }
        public string NombreEva { get; set; }
        public List<Notas> Notas { get; set; }
    }
}
