#region Using Directives

using System;
using MvvmCross.Plugin.Messenger;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Messages
{
    public class RefreshStateMessage : MvxMessage
    {
        #region Constructors

        public RefreshStateMessage(object sender, bool isBusy) : base(sender)
        {
            this.IsBusy = isBusy;
        }

        #endregion //Constructors

        #region Properties

        public bool IsBusy
        {
            get;
            private set;
        }

        #endregion //Properties
    }
}
