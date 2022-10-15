using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using MvvmCross.Plugin.Messenger;

namespace Nacelle.KMA.Core.Models.Messages
{
    public class ValidationErrorMessage : MvxMessage
    {
        public ValidationErrorMessage(object sender, ValidationFailure validationFalure): base(sender)
        {
            ValidationFailure = validationFalure;
        }

        public ValidationFailure ValidationFailure { get; }
    }
}
