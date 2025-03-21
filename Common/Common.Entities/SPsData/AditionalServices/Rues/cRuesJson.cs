using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SPsData.AditionalServices.Rues
{
    public class cRuesJson
    {
        [JsonProperty("codigo_error")]
        public string codigo_error { get; set; }
        [JsonProperty("mensaje_error")]
        public string mensaje_error { get; set; }
        [JsonProperty("rows")]
        public cRowsRuesJson[] rows { get; set; }
    }
}
