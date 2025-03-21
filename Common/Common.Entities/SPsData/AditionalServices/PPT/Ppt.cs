using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.PPT
{
    public class Ppt
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IdentificationType")]
        public string IdentificationType { get; set; }
        [JsonProperty("Identification")]
        public string Identification { get; set; }
        [JsonProperty("Correo")]
        public string Correo { get; set; }
        [JsonProperty("Telefono")]
        public string Telefono { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        //public string Error { get; set; }
    }
}
