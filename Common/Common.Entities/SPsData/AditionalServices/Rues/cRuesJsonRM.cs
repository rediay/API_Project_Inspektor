using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SPsData.AditionalServices.Rues
{
    public class cRuesJsonRM
    {
        [JsonProperty("codigo_error")]
        public string codigo_error { get; set; }
        [JsonProperty("mensaje_error")]
        public string mensaje_error { get; set; }
        [JsonProperty("rows")]
        public cRowsRuesJsonRM[] rows { get; set; }
    }
}
