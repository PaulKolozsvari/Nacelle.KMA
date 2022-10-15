using System.Collections.Generic;
using Nacelle.Core.Services;
using Nacelle.KMA.API.Configs;

namespace Nacelle.KMA.API.Services
{
    public abstract class Service
    {
        public IHttpClientService HttpClientService { get; private set; }
        public Config Config { get; private set; }

        protected Service(IHttpClientService httpClientService, Config config)
        {
            HttpClientService = httpClientService;
            Config = config;
            QueryParams = new Dictionary<string, string> { { "api_key", Config.API_Key } };
        }

        public IDictionary<string, string> QueryParams { get; } 
        public IDictionary<string, string> Headers { get; set; }
    }
}
