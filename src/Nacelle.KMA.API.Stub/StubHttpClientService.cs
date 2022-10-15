using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.Core;
using Nacelle.Core.Helpers;
using Nacelle.Core.Models;
using Nacelle.Core.Services;

namespace Nacelle.KMA.API.Stub
{
    public class StubHttpClientService : IHttpClientService
    {
        public async Task<Response<K>> GetAsync<K>(IDictionary<string, string> parameters, string baseUrl, string method, IDictionary<string, string> headers = null) where K : new()
        {
            var resourcePath = $"Nacelle.KMA.API.Stub.Resources.{method.ToLowerInvariant()}.json";
            var stubResponseObject = ResourceHelper.GetResourceObject<K, StubHttpClientService>(resourcePath);
            await Task.Delay(1500);
            return new Response<K>(stubResponseObject, true);
        }

        public async Task<Response<K>> PostRequestAsync<T, K>(T request, IDictionary<string, string> parameters, string baseUrl, string method, IDictionary<string, string> headers = null) where K: new()
        {
            var resourcePath = $"Nacelle.KMA.API.Stub.Resources.{method.ToLowerInvariant()}.json";
            var stubResponseObject = ResourceHelper.GetResourceObject<K, StubHttpClientService>(resourcePath);
            await Task.Delay(1500);
            var response = new Response<K>(stubResponseObject, true);
            response.Headers.Add("ConversationID", Guid.NewGuid().ToString());
            return response;
        }

        public async Task<byte[]> PostBytesAsync(IDictionary<string, string> parameters, IDictionary<string, string> headers, string baseUrl, string method, string json)
        {
            var resourcePath = $"Nacelle.KMA.API.Stub.Resources.{method.ToLowerInvariant()}.txt";
            var bytes = ResourceHelper.GetByteArrayResource<StubHttpClientService>(resourcePath);
            await Task.Delay(1500);

            return bytes;
        }
    }
}
