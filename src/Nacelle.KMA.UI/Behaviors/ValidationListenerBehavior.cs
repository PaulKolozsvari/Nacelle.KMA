using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Models.Messages;
using Nacelle.KMA.UI.Views;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Behaviors
{
    public class ValidationListenerBehavior: Behavior<ValidationEntry>
    {
        private IMvxMessenger _validationHandler;
        private ValidationEntry _validationEntry;
        private MvxSubscriptionToken _subscriptionToken;

        public ValidationListenerBehavior()
        {
            _validationHandler = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        public static readonly BindableProperty PropertyNameProperty = BindableProperty.CreateAttached("Name", typeof(string), typeof(ValidationEntry), string.Empty);

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        protected override void OnAttachedTo(ValidationEntry bindable)
        {
            base.OnAttachedTo(bindable);

            _validationEntry = bindable;
            _subscriptionToken = _validationHandler.Subscribe<ValidationErrorMessage>(ValidationErrorMessageHandler);
            _validationEntry.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _validationEntry.IsValid = true;
        }

        protected override void OnDetachingFrom(ValidationEntry bindable)
        {
            base.OnDetachingFrom(bindable);

            _validationHandler.Unsubscribe<ValidationErrorMessage>(_subscriptionToken);
        }

        private void ValidationErrorMessageHandler(ValidationErrorMessage obj)
        {
            if (obj.ValidationFailure.PropertyName == PropertyName)
            {
                _validationEntry.IsValid = false;
            }
        }
    }
}
