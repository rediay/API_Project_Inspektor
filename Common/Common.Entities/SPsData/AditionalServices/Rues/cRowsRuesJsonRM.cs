using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SPsData.AditionalServices.Rues
{
    public class cRowsRuesJsonRM
    {
        [JsonProperty("categoria_matricula")]
        public string categoria_matricula { get; set; }

        [JsonProperty("identificacion")]
        public string identificacion { get; set; }
        [JsonProperty("estado")]
        public string estado { get; set; }
        [JsonProperty("matricula")]
        public string matricula { get; set; }
        [JsonProperty("nombre_camara")]
        public string nombre_camara { get; set; }
        [JsonProperty("organizacion_juridica")]
        public string organizacion_juridica { get; set; }
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
    }
}
