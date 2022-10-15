using Nacelle.Core.Models;

namespace Nacelle.KMA.Core.Tests
{
    public static class ResponseGenerator<T>
    {
        public static Response<T> Success(T data)
        {
            return new Response<T>(data, true);
        }
    }
}
