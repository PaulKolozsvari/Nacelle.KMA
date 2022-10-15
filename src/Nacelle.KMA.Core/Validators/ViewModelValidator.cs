using System;
using System.Collections.Generic;
using FluentValidation;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Models.Messages;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.Core.Validators
{
    public class ViewModelValidator : IViewModelValidator
    {
        private readonly IMvxMessenger _messenger;

        public ViewModelValidator(IMvxMessenger messenger)
        {
            _messenger = messenger;
            ErrorMessages = new List<string>();
        }

        public bool Validate<T>(IValidator validator, T model) //where T : BaseViewModel
        {
            ErrorMessages.Clear();

            var results = validator.Validate(model);
            if (results.IsValid)
            {
                return true;
            }
            foreach (var error in results.Errors)
            {
                ErrorMessages.Add(error.ErrorMessage);
                _messenger.Publish(new ValidationErrorMessage(this, error));
            }

            return false;
        }

        public bool Validate<T>(IValidator validator, T model, out Dictionary<string, string> errors)
        {
            errors = null;

            ErrorMessages.Clear();

            var results = validator.Validate(model);
            if (results.IsValid)
            {
                return true;
            }

            errors = new Dictionary<string, string>();

            foreach (var error in results.Errors)
            {
                ErrorMessages.Add(error.ErrorMessage);

                errors.Add(error.PropertyName, error.ErrorMessage);
            }

            return false;
        }

        public bool Validate<T>(T model) where T : BaseViewModel
        {
            throw new NotImplementedException();
        }

        public IList<string> ErrorMessages { get; }
    }
}
