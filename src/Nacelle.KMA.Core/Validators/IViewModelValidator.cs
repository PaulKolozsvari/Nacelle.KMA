using System.Collections.Generic;
using FluentValidation;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.Core.Validators
{
    public interface IViewModelValidator
    {
        bool Validate<T>(IValidator validator, T model);
        bool Validate<T>(IValidator validator, T model, out Dictionary<string, string> errors);
        bool Validate<T>(T model) where T : BaseViewModel;
        IList<string> ErrorMessages { get; }
    }
}
