using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Sunat
{
    public class cDataSunatJson
    {
        [JsonProperty("no_ruc")]
        public string no_ruc { get; set; }
        [JsonProperty("tipo_cont")]
        public string tipo_cont { get; set; }
        [JsonProperty("nombre_comercial")]
        public string nombre_comercial { get; set; }
        [JsonProperty("fecha_inscripcion")]
        public string fecha_inscripcion { get; set; }
        [JsonProperty("fecha_inicio_actividades")]
        public string fecha_inicio_actividades { get; set; }
        [JsonProperty("estado")]
        public string estado { get; set; }
        [JsonProperty("condicion")]
        public string condicion { get; set; }
        [JsonProperty("domicilio")]
        public string domicilio { get; set; }
        [JsonProperty("sistema_emicion")]
        public string sistema_emicion { get; set; }
        [JsonProperty("actividad_comercio_exterior")]
        public string actividad_comercio_exterior { get; set; }
        [JsonProperty("sistema_contabilidad")]
        public string sistema_contabilidad { get; set; }
        [JsonProperty("actividades_economicas")]
        public string actividades_economicas { get; set; }
        [JsonProperty("comprobante_pago")]
        public string comprobante_pago { get; set; }
        [JsonProperty("sistema_emision_electronica")]
        public string sistema_emision_electronica { get; set; }
        [JsonProperty("emision_electronica_desde")]
        public string emision_electronica_desde { get; set; }
        [JsonProperty("comprobantes_electronicos")]
        public string comprobantes_electronicos { get; set; }
        [JsonProperty("afiliado_ple")]
        public string afiliado_ple { get; set; }
        [JsonProperty("padrones")]
        public string padrones { get; set; }
    }
}
