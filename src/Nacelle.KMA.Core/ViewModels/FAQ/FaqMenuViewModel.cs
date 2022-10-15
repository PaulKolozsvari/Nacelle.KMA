#region Using Directives

using System.Threading.Tasks;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Data;
using Xamarin.Essentials;
using System.Collections.Generic;
using Nacelle.KMA.Core.Models;
using System;
using Nacelle.KMA.Core.Commands;
using System.Windows.Input;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FaqMenuViewModel: BaseClosableViewModel
    {
        #region Constructors

        public FaqMenuViewModel()
        {
            KululaURLCommand = new TrackableAsyncCommand(Constants.Analytics.Events.FAQItemTap, DoKululaURLCommandAsync, Constants.Analytics.Target.KululaWebsite);
            DisplayFaqCommand = new TrackableAsyncCommand<QuestionItem>(Constants.Analytics.Events.FAQItemTap, DoDisplayFaqCommandAsync);
        }

        #endregion //Constructors

        #region Commands

        public ICommand KululaURLCommand { get; }
        public TrackableAsyncCommand<QuestionItem> DisplayFaqCommand { get; }
        public IEnumerable<QuestionItem> QuestionItems { get; private set; }

        #endregion //Commands

        #region Methods

        public override Task Initialize()
        {
            Task result = base.Initialize();
            FaqDataModel faqDataModel = DataFactory.CreateFaqDataModel();
            QuestionItems = faqDataModel.QuestionItems;
            return result;
        }

        private Task DoKululaURLCommandAsync()
        {
            return Browser.OpenAsync("https://kulula.com");
        }

        private Task DoDisplayFaqCommandAsync(QuestionItem arg)
        {
            return NavigationService.Navigate<FaqDetailViewModel, QuestionItem>(arg);
        }

        #endregion //Methods
    }
}
