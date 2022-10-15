using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Nacelle.Core.Helpers
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Gets the resource object by deserializeing the input resource
        /// </summary>
        /// <returns>The resource object.</returns>
        /// <param name="resourcePath">Resource path.</param>
        /// <typeparam name="T">The type return object being deserilized into</typeparam>
        /// <typeparam name="K">Any type that can be found in the assembly where the reosource is located</typeparam>
        public static T GetResourceObject<T, K>(string resourcePath)
        {
            var assembly = typeof(K).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        /// <summary>
        /// Gets the string resource.
        /// </summary>
        /// <returns>The string resource.</returns>
        /// <param name="resourcePath">Resource path.</param>
        /// <typeparam name="K">Any type that can be found in the assembly where the reosource is located</typeparam>
        public static string GetStringResource<K>(string resourcePath)
        {
            var assembly = typeof(K).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Gets the string resource.
        /// </summary>
        /// <returns>The string resource.</returns>
        /// <param name="resourcePath">Resource path.</param>
        /// /// <typeparam name="K">Any type that can be found in the assembly where the reosource is located</typeparam>
        public static byte[] GetByteArrayResource<K>(string resourcePath)
        {
            var assembly = typeof(K).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                var ba = new byte[stream.Length];
                stream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
    }
}
