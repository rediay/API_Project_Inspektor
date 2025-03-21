using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.CriminalRecordEcuador
{
    public class CriminalRecordEcuador
    {
        [JsonProperty("criminalRecord")]
        public string criminalRecord { get; set; }
        [JsonProperty("documentType")]
        public string documentType { get; set; }
        [JsonProperty("document")]
        public string document { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
    }
}
