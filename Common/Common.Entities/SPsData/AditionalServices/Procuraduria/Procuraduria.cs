using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Entities.SPsData.AditionalServices.Procuraduria
{
    public class Procuraduria
    {
        public string html_response { get; set; }
        [JsonProperty("not_criminal_records")]
        public ProcuraduriaNotCriminalRecordsData? not_criminal_records { get; set; }
        [JsonProperty("data")]
        public List<ProcuraduriaData> data { get; set; }
        public string errorMessage { get; set; }
    }
}
