using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class CertificadoExamen
    {
        public string NombreAdministrador { get; set; }
        public string NombreAlumno { get; set; }
        public int DniAlumno { get; set; }
        public string Carrera { get; set; }
        public string Interesado { get; set; }
        public int DiaNumero {  get; set; }
        public string Mes {  get; set; }
        public int Anio { get; set; }
    }
}
