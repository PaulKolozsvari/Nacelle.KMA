using System;
using System.Text;

namespace Nacelle.Core.Helpers
{
    public static class EncodingHelper
    {
        public static string FromBase64String(string base64String)
        {
            var buffer = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public static string ToBase64String(string inputString)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
