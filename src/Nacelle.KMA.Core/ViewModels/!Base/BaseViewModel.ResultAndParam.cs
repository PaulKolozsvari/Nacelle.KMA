#region Using Directives

using MvvmCross.ViewModels;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
    {
        #region Properties

        public TParameter Parameter { get; protected set; }

        #endregion //Properties

        #region Methods

        public virtual void Prepare(TParameter parameter)
        {
            Parameter = parameter;
        }

        #endregion //Methods
    }
}
