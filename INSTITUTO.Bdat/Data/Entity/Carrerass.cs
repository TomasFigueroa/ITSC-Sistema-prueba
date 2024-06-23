using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Carrerass
    {
        public int IdCarrera { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
       
    }
}
