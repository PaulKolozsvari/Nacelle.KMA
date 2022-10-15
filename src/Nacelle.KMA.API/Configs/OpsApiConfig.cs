namespace Nacelle.KMA.API.Configs
{
    public class OpsApiConfig : Config
    {
        public override string BaseUrl => Constants.OpsApiConstants.API_BaseUrl;
        public override string API_Key => Constants.OpsApiConstants.API_KEY;
        public string SessionKey => Constants.OpsApiConstants.OpsApiSessionToken;
    }
}
