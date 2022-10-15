using System;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Nacelle.KMA.Core.Models.Messages
{
    public class ShowViewModelMessage : MvxMessage
    {
        public ShowViewModelMessage(object sender, Type viewModel) : base(sender)
        {
            ViewModelType = viewModel;
        }

        public Type ViewModelType { get; }
    }
}
