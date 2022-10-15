using System.Collections.Generic;

namespace Nacelle.Core.Models
{
    public class Response<T>
    {
        public Response(T data, bool isSuccess, string error = null)
        {
            Data = data;
            IsSuccess = isSuccess ;
            Message = error;

            Headers = new Dictionary<string, string>();
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IDictionary<string, string> Headers { get; set; }
    }
}
