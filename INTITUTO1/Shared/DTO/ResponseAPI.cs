using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTITUTO1.Shared.DTO
{
    public class ResponseAPI<T>
    {
        public bool EsCorrecto { get; set; }
        public T? Valor { get; set; }
        public String? Mensaje { get; set; }

    }
}
