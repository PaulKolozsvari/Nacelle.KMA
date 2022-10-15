using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.Core.Models;

namespace Nacelle.Core.Services
{
    public interface IHttpClientService
    {
        Task<Response<K>> GetAsync<K>(IDictionary<string, string> parameters,
                                      string baseUrl,
                                      string method,
                                      IDictionary<string, string> headers = null) where K : new();

        Task<Response<K>> PostRequestAsync<T, K>(T request,
                                                IDictionary<string, string> parameters,
                                                string baseUrl,
                                                string method,
                                                IDictionary<string, string> headers = null) where K : new();

        Task<byte[]> PostBytesAsync(
            IDictionary<string, string> parameters,
            IDictionary<string, string> headers,
            string baseUrl, string method, string json);
    }
}
