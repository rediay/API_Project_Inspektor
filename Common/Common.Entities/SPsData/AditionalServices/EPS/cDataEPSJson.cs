using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.EPS
{
    public class cDataEPSJson
    {
        [JsonProperty("Entity")]
        public string Entity { get; set; }
        [JsonProperty("Regime")]
        public string Regime { get; set; }
        [JsonProperty("EffectiveDate")]
        public string EffectiveDate { get; set; }
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }
        [JsonProperty("AffiliateType")]
        public string AffiliateType { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
    }
}
