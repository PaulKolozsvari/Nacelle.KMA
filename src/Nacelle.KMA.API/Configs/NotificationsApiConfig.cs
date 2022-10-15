namespace Nacelle.KMA.API.Configs
{
    public class NotificationsApiConfig : Config
    {
        public override string BaseUrl => Constants.NotificationsApiConstants.API_BaseUrl;
        public override string API_Key => Constants.NotificationsApiConstants.API_KEY;
        public string SessionKey => Constants.NotificationsApiConstants.NotificationsApiSessionToken;
    }
}
