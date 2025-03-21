using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Entities.SPsData.AditionalServices.Procuraduria
{
    public class Military
    {
        [JsonProperty("ResultName")]
        public string Name { get; set; }
        [JsonProperty("StatusResult")]
        public string ReservationType { get; set; }
        [JsonProperty("PlaceResult")]
        public string Place { get; set; }
        [JsonProperty("AddressResult")]
        public string Address { get; set; }

        public string Error { get; set; }
        
    }
}
