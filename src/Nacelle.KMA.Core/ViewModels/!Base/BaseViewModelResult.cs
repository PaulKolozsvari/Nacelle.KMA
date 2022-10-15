#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
    {
        #region Properties

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        #endregion //Properties

        #region Methods

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource?.Task.IsCompleted == false && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }

        #endregion //Methods
    }
}
