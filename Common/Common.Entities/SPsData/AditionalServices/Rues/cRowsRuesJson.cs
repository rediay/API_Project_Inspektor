using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SPsData.AditionalServices.Rues
{
    public class cRowsRuesJson
    {
        [JsonProperty("identificacion")]
        public string identificacion { get; set; }
        [JsonProperty("razon_social")]
        public string razon_social { get; set; }
        [JsonProperty("sigla")]
        public string sigla { get; set; }
        [JsonProperty("categoria_matricula")]
        public string matricula { get; set; }
        [JsonProperty("municipio")]
        public string municipio { get; set; }
        [JsonProperty("estadoRM")]
        public string estado { get; set; }
    }
}
