using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BaseResponse<TResult> where TResult : Result
    {
        [JsonProperty("results")]
        public List<TResult> Results { get; set; }

        public bool IsSuccess => !(Results != null && Results.Any() && Results.SelectMany(x => x.Status).Any(x => x.Type == "ERROR"));

        public string Message => Results != null && Results.Any() 
            ? Results.SelectMany(x => x.Status).Select(x => x.Message).FirstOrDefault() 
            : null;

        public bool HasExpiredConversatonID =>
            !IsSuccess &&
            (
                Message.StartsWith(Constants.ErrorDescriptions.CachedObjectNotFoundPrefix, StringComparison.InvariantCultureIgnoreCase)
                ||
                Message.StartsWith(Constants.ErrorDescriptions.ClientReceived404, StringComparison.InvariantCultureIgnoreCase)
            );

    }

    public class Result
    {
        [JsonProperty("status")]
        public List<Status> Status { get; set; }
    }

    public class Status
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
