using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSTITUTO.Bdat.Data.Entity
{
    public class Alumnos
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI_Alum { get; set; }
        public string Cuil { get; set; }
        public DateTime Fecha_Nac { get; set; }
        public string Tbase { get; set; }
        public string Nacionalidad { get; set; }
        public bool Estado { get; set; }
        public string Numero { get; set; }
        public string Sexo { get; set; }
        public int Id_Carrera { get; set; }
        public List<DivsionCiclosMateriaAlumnos> DivsionCiclosMateriaAlumnos { get; set; }
    }
}
