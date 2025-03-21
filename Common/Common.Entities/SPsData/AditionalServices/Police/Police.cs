using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Police
{
    public class Police
    {
        public Message message { get; set; }
        public bool inRisk { get; set; }
    }
    public class Message
    {
        public string text_result { get; set; }
    }
}
