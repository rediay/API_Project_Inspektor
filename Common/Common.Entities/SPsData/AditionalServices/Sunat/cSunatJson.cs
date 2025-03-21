using Common.Entities.SPsData.AditionalServices.EPS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Sunat
{
    public class cSunatJson
    {
        [JsonProperty("hasError")]
        public string codigo_error { get; set; }
        [JsonProperty("message")]
        public string mensaje_error { get; set; }
        [JsonProperty("data")]
        public cDataSunatJson rows { get; set; }
    }
}
