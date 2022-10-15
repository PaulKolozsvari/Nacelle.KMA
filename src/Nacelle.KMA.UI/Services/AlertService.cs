using System;
using System.Linq;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Platform;

namespace Nacelle.KMA.UI.Services
{
    public class AlertService : IAlertService
    {
        public async Task Show(string title, string message, (string Title, Func<Task> Action) acceptAction, (string Title, Func<Task> Action)? cancelAction = null)
        {
            var mainPage = Xamarin.Forms.Application.Current.MainPage;
            if (mainPage.Navigation != null)
            {
                var navigation = mainPage.Navigation.NavigationStack.LastOrDefault();
                if (navigation != null)
                {
                    mainPage = navigation;
                }
            }

            if (string.IsNullOrEmpty(cancelAction?.Title))
            {
                await mainPage.DisplayAlert(title, message, acceptAction.Title);
                acceptAction.Action?.Invoke();
            }
            else
            {
                var result = await mainPage.DisplayAlert(title, message, acceptAction.Title, cancelAction?.Title);
                if (result)
                {
                    acceptAction.Action?.Invoke();
                }
                else
                {
                    cancelAction?.Action?.Invoke();
                }
            }
        }
    }
}
