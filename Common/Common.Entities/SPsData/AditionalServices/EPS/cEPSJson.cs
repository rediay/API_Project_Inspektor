using Common.Entities.SPsData.AditionalServices.Rues;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.EPS
{
    public class cEPSJson
    {
        [JsonProperty("hasError")]
        public string codigo_error { get; set; }
        [JsonProperty("message")]
        public string mensaje_error { get; set; }
        [JsonProperty("data")]
        public IEnumerable<Eps> rows { get; set; }
        //public IEnumerable<cDataEPSJson> rows { get; set; }
    }
}
