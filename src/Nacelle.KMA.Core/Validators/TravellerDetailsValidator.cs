using System;
using FluentValidation;
using Nacelle.KMA.Core.Models.Items;

namespace Nacelle.KMA.Core.Validators
{
    public class TravellerDetailsValidator : AbstractValidator<TravellerItem>
    {
        public TravellerDetailsValidator()
        {
            RuleFor(x => x.EmergencyContact.Name)
                .NotEmpty()
                .When(x => x.RequiresEmergencyContact);

            RuleFor(x => x.EmergencyContact.Country)
                .NotEmpty()
                .When(x => x.RequiresEmergencyContact);

            RuleFor(x => x.EmergencyContact.DialingCode)
                .NotEmpty()
                .When(x => x.RequiresEmergencyContact);

            RuleFor(x => x.EmergencyContact.ContactNumber)
                .NotEmpty()
                .When(x => x.RequiresEmergencyContact);

            RuleFor(x => x.PassportDetails.FirstName)
                .NotEmpty()
                .When(x => x.RequiresPassport);

            RuleFor(x => x.PassportDetails.LastName)
                .NotEmpty()
                .When(x => x.RequiresPassport);

            RuleFor(x => x.PassportDetails.Nationality)
                .NotEmpty()
                .When(x => x.RequiresPassport);

            RuleFor(x => x.PassportDetails.PassportNumber)
                .NotEmpty()
                .When(x => x.RequiresPassport);

            RuleFor(x => x.PassportDetails.ExpirationDay)
                .NotEmpty()
                .When(x => x.RequiresPassport);

            RuleFor(x => x.PassportDetails.ExpirationDate)
                .Must(x => x > DateTime.Now)
                .When(x => x.RequiresPassport).
                WithMessage("Passport has expired");
        }
    }
}
