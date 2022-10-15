#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nacelle.Core.Exceptions;
using Nacelle.Core.Models;
using Newtonsoft.Json;

#endregion //Using Directives

namespace Nacelle.Core.Services
{
    public class HttpClientService : IHttpClientService
    {
        #region Fields

        private HttpClient _client;

        #endregion //Fields

        #region Properties

        protected HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }
                return _client;
            }
        }

        #endregion //Properties

        #region Methods

        public async Task<Response<K>> GetAsync<K>(IDictionary<string, string> parameters, string baseUrl, string method, IDictionary<string, string> headers = null) where K : new()
        {
            return await SendRequestAsync<K>(parameters, headers, baseUrl, method, HttpMethod.Get);
        }

        public async Task<Response<K>> PostRequestAsync<T, K>(T request, IDictionary<string, string> parameters, string baseUrl, string method, IDictionary<string, string> headers = null) where K : new()
        {
            var json = JsonConvert.SerializeObject(request);
           return await SendRequestAsync<K>(parameters, headers, baseUrl, method, HttpMethod.Post, json);
        }

        public async Task<byte[]> PostBytesAsync(IDictionary<string, string> parameters, IDictionary<string, string> headers, string baseUrl, string method, string json)
        {
#if DEBUG
            Debug.WriteLine("PostBytesAsync JSON:");
            Debug.WriteLine("--------------");
            Debug.WriteLine(json);
#endif
            var uri = GenerateQueryString(string.Concat(baseUrl, "/", method), parameters);
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }
                var response = await Client.SendAsync(requestMessage);
                var content = await response.Content.ReadAsByteArrayAsync();
                return content;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oops => " + ex.Message + " " + ex.StackTrace);
                throw new KululaNetworkException(ex);
            }
        }

        private async Task<Response<K>> SendRequestAsync<K>(
            IDictionary<string, string> parameters,
            IDictionary<string, string> headers,
            string baseUrl, string method,
            HttpMethod httpMethod,
            string json = null) where K : new()
        {
            Response<K> result = null;
            try
            {
                var uri = GenerateQueryString(string.Concat(baseUrl, "/", method), parameters);
                var requestMessage = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(uri),
                };
                Debug.WriteLine($"HTTP {httpMethod.Method}: " + uri);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    Debug.WriteLine("HTTP Body: " + json);
                }
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                    Debug.WriteLine(headers.Aggregate("HTTP Headers: ", (x, y) => $"{x}{y.Key}={y.Value};"));
                }
                using (var response = await Client.SendAsync(requestMessage))
                {
#if DEBUG
                    var logMessage = new StringBuilder();
                    logMessage.AppendLine($"HTTP Response Status Code: {response.StatusCode}");
                    logMessage.AppendLine($"HTTP Response Status Message: {response.Content.ToString()}");
                    Debug.WriteLine(logMessage.ToString());

#endif
                    result = await TryParseResponseAsync<K>(response);
                    if (response.IsSuccessStatusCode)
                    {
                        result.Headers = response.Headers.ToDictionary(x => x.Key.ToLower(), x => x.Value.FirstOrDefault());
                    }
                    else
                    {
                        throw new Exception($"Failed HTTP Response: {response.StatusCode} - {response.ReasonPhrase}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oops => " + ex.Message + " " + ex.StackTrace);
                throw new KululaNetworkException(ex);
            }
            return result;
        }

        private string GenerateQueryString(string uri, IDictionary<string, string> parameters)
        {
            if (parameters.Count > 0)
            {
                uri += "?";
                foreach (var param in parameters)
                {
                    uri += $"{param.Key}={param.Value}&";
                }
            }
            return uri.TrimEnd(new char[] { '&' });
        }

        private async Task<Response<K>> TryParseResponseAsync<K>(HttpResponseMessage response) where K : new()
        {
            var stream = await response.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            {
#if DEBUG
                var text = streamReader.ReadToEnd();

                Debug.WriteLine("Response: " + text);
                stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);
#endif
                if (response.IsSuccessStatusCode)
                {
                    using (JsonReader jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var responseObject = serializer.Deserialize<K>(jsonReader);
                        return new Response<K>(responseObject, response.IsSuccessStatusCode, response.IsSuccessStatusCode ? null : response.ReasonPhrase);
                    }
                }
                else
                {
                    return new Response<K>(new K(), false, response.ReasonPhrase);
                }
            }
        }

        #endregion //Methods
    }
}
