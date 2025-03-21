using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.IndividualQueryExternal
{
    public class IndividualQueryExternalParamsEsDTO
    {
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public int TipoDocumento { get; set; }
        public int CantidadPalabras { get; set; }
        public bool TienePrioridad_4 { get; set; }
    }
}
