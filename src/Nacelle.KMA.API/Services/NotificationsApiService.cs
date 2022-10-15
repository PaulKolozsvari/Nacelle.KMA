using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Nacelle.Core.Services;
using Nacelle.KMA.API.Configs;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;

namespace Nacelle.KMA.API.Services
{
    public class NotificationsApiService : Service, INotificationsApiService
    {
        private string SessionToken { get; }

        public NotificationsApiService(IHttpClientService clientService, NotificationsApiConfig config) : base(clientService, config)
        {
            SessionToken = config.SessionKey;
            Headers = new Dictionary<string, string>()
            {
                {
                    Constants.Headers.SessionToken, SessionToken
                }
            };
        }

        public Task<byte[]> BoardingPassWalletBytesAsync(string boardingPassJson)
        {
            var url = string.Concat(Config.BaseUrl, "/", Constants.NotificationsApiConstants.PATH_FormattedBoardingPass);

            return HttpClientService.PostBytesAsync(QueryParams, Headers, url, "getboardingpass", boardingPassJson);
        }

        public async Task<bool> RegisterPushNotificationsTokenAsync(string pnToken, string deviceType)
        {
            var url = string.Concat(Config.BaseUrl, "/", Constants.NotificationsApiConstants.PATH_Device);

            var request = new RegisterPushNotificationTokenRequest
            {
                DeviceIdentity = pnToken,
                DeviceType = deviceType
            };

            try
            {
                var resp = await HttpClientService.PostRequestAsync<RegisterPushNotificationTokenRequest, RegisterPushNotificationTokenResponse>(request, QueryParams, url, "RegisterDeviceId", Headers);
                return resp.IsSuccess;
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }

            return false;
        }
    }
}
