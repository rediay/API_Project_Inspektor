using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Sunat
{
    public class Sunat
    {
        public string no_ruc { get; set; }
        public string tipo_cont { get; set; }
        public string nombre_comercial { get; set; }
        public string fecha_inscripcion { get; set; }
        public string fecha_inicio_actividades { get; set; }
        public string estado { get; set; }
        public string condicion { get; set; }
        public string domicilio { get; set; }
        public string sistema_emicion { get; set; }
        public string actividad_comercio_exterior { get; set; }
        public string sistema_contabilidad { get; set; }
        public string actividades_economicas { get; set; }
        public string comprobante_pago { get; set; }
        public string sistema_emision_electronica { get; set; }
        public string emision_electronica_desde { get; set; }
        public string comprobantes_electronicos { get; set; }
        public string afiliado_ple { get; set; }
        public string padrones { get; set; }
    }
}
