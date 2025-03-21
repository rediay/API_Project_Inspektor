using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Queries
{
    public class BulkQueryListExcelCoincidenceDTO
    {
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public int Cantidad_coincidencias { get; set; }
    }
}
