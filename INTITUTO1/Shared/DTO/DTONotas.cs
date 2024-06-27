using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class DTONotas
    {
        public int Nota {  get; set; }
        public DateTime Fecha { get; set; }
        public int TipoEvaluacionIdTipoEva {  get; set; }

        //agrega tipo de evaluacion si hace falta aca
    }
}
