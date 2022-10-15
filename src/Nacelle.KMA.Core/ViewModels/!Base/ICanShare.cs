#region Using Directives

using System.Collections.Generic;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public interface ICanShare
    {
        #region Properties

        List<TravellerItem> TravellerItems { get; }

        #endregion //Properties

        #region Commands

        TrackableAsyncCommand<List<TravellerItem>> ShareBoardingPassCommand { get; }

        #endregion //Commands
    }
}
