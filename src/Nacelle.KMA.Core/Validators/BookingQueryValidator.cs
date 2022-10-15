using System;
using FluentValidation;
using Nacelle.KMA.Core.ViewModels;
namespace Nacelle.KMA.Core.Validators
{
    public class BookingQueryValidator<T>: AbstractValidator<T> where T: IBookingQuery
    {
        public BookingQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.BookingReference)
              .NotNull()
              .NotEmpty()
              .Must(NotContainSpaces)
              .WithMessage("'Booking Reference' must not contain spaces")
              .Length(6);

            RuleFor(x => x.LastName)
                .NotEmpty();
        }

        private bool NotContainSpaces(string arg)
        {
            return arg != null && !arg.Contains(" ");
        }
    }
}
