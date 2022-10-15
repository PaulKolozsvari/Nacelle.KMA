using System.Threading;
using System.Threading.Tasks;
using MvvmCross;
using Nacelle.KMA.Core.Repositories;
using Nacelle.KMA.Core.ViewModels;
using FluentValidation;

namespace Nacelle.KMA.Core.Validators
{
    public class FindBookingValidator : BookingQueryValidator<FindBookingViewModel>
    {
        private IBookingRepository _bookingRepo;

        public FindBookingValidator()
        {
            _ = RuleFor(x => x.BookingReference)
                .NotNull()
                .NotEmpty()
                .MustAsync(BookingNotFoundInLocalStorageAsync)
                .WithMessage("Booking already added as a trip, go to the My Trips section");
        }

        private IBookingRepository BookingRepo 
        {
            get
            {
                if (_bookingRepo == null)
                {
                    _bookingRepo = Mvx.IoCProvider.Resolve<IBookingRepository>();
                }

                return _bookingRepo;
            }
        }

        private async Task<bool> BookingNotFoundInLocalStorageAsync(string bookingReference, CancellationToken arg2)
        {
            var found = await BookingRepo.ContainsBookingAsync(bookingReference);
            return !found;
        }

    }
}
