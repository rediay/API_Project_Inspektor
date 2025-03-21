using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.Helpers
{
    public class RequestAditionalServiceParams
    {
        public string url { get; set; }
        public HttpMethod method { get; set; }
        public string? token { get; set; }
        public StringContent? content { get; set; }
    }
}
