using System.Collections.Generic;

namespace Nacelle.KMA.API.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        public static string GetConversationID(this IDictionary<string, string> headers)
        {
            return headers.TryGetValue(Constants.Headers.ConversationID, out var conversationId) ? conversationId : null;
        }
    }
}
