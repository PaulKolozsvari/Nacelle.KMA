namespace Nacelle.KMA.API
{
    public static class Constants
    {
#if DEBUG
        public static readonly string BASE_API_KEY = "8xzterbzb6r9m93kkxpfhr5d";
        public static readonly string BASE_API_BaseUrl = "https://cert-api.kulula.com";
#else
        // CERT
        public static readonly string BASE_API_KEY = "8xzterbzb6r9m93kkxpfhr5d";
        public static readonly string BASE_API_BaseUrl = "https://cert-api.kulula.com";
        //PROD
        //public static readonly string API_KEY = "u8v2zfadb4nrzbd5rsfmy5st";
        //public static readonly string API_BaseUrl = "https://api.kulula.com";

#endif

        public static class TibcoConstants
        {
            public static readonly string API_KEY = BASE_API_KEY;
            public static readonly string API_BaseUrl = BASE_API_BaseUrl;
        }

        public static class OpsApiConstants
        {
            public static readonly string PATH_Weather = "opsapi/weather";
            public static readonly string PATH_Flight = "opsapi/flight";
            public static readonly string API_KEY = BASE_API_KEY;
            public static readonly string API_BaseUrl = BASE_API_BaseUrl;

#if DEBUG
            public static readonly string OpsApiSessionToken = "CF2944B1-54FE-4F6E-9B8C-E3CD5302481D";
#else
            // CERT
            public static readonly string OpsApiSessionToken = "CF2944B1-54FE-4F6E-9B8C-E3CD5302481D";
            //PROD
            // public static readonly string OpsApiSessionToken =  "E5F4FF13-B494-4903-9973-F9FAC8F8A5B0";
#endif
        }

        public static class NotificationsApiConstants
        {
            public static readonly string API_BaseUrl = BASE_API_BaseUrl;
            public static readonly string API_KEY = BASE_API_KEY;
            public static readonly string PATH_FormattedBoardingPass = "notifications/pass";
            public static readonly string PATH_Device = "notifications/device";
#if DEBUG
            public static readonly string NotificationsApiSessionToken = "218835B7-C83D-4992-BAC8-2A87B0A665E5";
#else
            // CERT
            public static readonly string NotificationsApiSessionToken = "218835B7-C83D-4992-BAC8-2A87B0A665E5";
            //PROD
            //public static readonly string NotificationsApiSessionToken = "26CBC7FE-556C-47C4-819B-C9C6DC268860";
#endif
        }

        public static class Headers
        {
            public static readonly string ConversationID = "conversationid";
            public static readonly string SessionToken = "sessiontoken";
        }

        public static class ErrorCodes
        {
            public static readonly string BusinessError = "ERR.DCCI.APP.BUSINESS_ERROR";
            public static readonly string FlighNotInitialized = "ERR.DCCI.BUSINESS_ERROR.FLIGHT_NOT_INITIALIZED";
        }

        public static class ErrorDescriptions
        {
            public static readonly string CachedObjectNotFoundPrefix = "Cannot find cached object for sessionID";
            public static readonly string ClientReceived404 = "Client received a 4xx response for invocation at resource path";
        }
    }
}
