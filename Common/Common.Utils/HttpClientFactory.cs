using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class HttpClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        public HttpClientFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public HttpClient CreateClient()
        {
            return _clientFactory.CreateClient();
        }
    }
}
