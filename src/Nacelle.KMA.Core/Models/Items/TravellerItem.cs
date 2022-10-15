using System;
using System.ComponentModel;
using MvvmCross.ViewModels;
using Nacelle.KMA.API.Models.Enums;
using Nacelle.KMA.Core.ExtensionMethods;
using Nacelle.KMA.Core.Models.Enums;

namespace Nacelle.KMA.Core.Models.Items
{
    public class TravellerItem : NavigationItem
    {
        private bool _doCheckIn;
        private bool _hasUpdatedWeightCategory;
        private SeatItem _seatItem;
        private int _selectedWeightCategoryIndex;
        private bool doShare;

        public TravellerItem()
        {
            SelectedWeightCategoryIndex = -1;
            SeatItem = new SeatItem();
        }

        public bool DoCheckIn { get => _doCheckIn; set => SetProperty(ref _doCheckIn, value); }
        public bool ShouldUpdateWeightCategory { get; set; }
        public bool HasUpdatedWeightCategory { get => _hasUpdatedWeightCategory; set => SetProperty(ref _hasUpdatedWeightCategory, value); }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}".ToTitleCase();
        public bool HasInfant { get; set; }
        public PassengerTypes PassengerType { get; internal set; }
        public bool IsInfant => PassengerType == PassengerTypes.Infant;
        public string InfantName { get; set; }
        public string InfantId { get; set; }


        public int SelectedWeightCategoryIndex
        {
            get => _selectedWeightCategoryIndex;
            set
            {
                if (SetProperty(ref _selectedWeightCategoryIndex, value))
                {
                    RaisePropertyChanged(() => SelectedWeightCategory);
                    RaisePropertyChanged(() => SelectedWeightCategoryName);
                }
            }
        }

        public string PassengerFlightId { get; set; }

        public string SegmentId { get; set; }

        public WeightCategory SelectedWeightCategory => (WeightCategory)SelectedWeightCategoryIndex;
        public string SelectedWeightCategoryName => SelectedWeightCategory.GetAttributeOfType<DescriptionAttribute>().Description;

        public bool RequiresPassport { get; set; }
        public bool RequiresEmergencyContact { get; set; }
        public PassportDetails PassportDetails { get; set; } = new PassportDetails();
        public EmergencyContact EmergencyContact { get; set; } = new EmergencyContact();

        public SeatItem SeatItem
        {
            get => _seatItem;
            set
            {
                if (SetProperty(ref _seatItem, value))
                {
                    RaisePropertyChanged(() => Seat);
                    RaisePropertyChanged(() => SeatDescription);
                }
            }
        }

        public string Seat => SeatItem == null ? string.Empty : SeatItem.Seat;
        public string SeatDescription => $"Seat {Seat} ({GetSeatLocation()})";

        public int Index { get; set; }

        public bool DoShare { get => doShare; set => SetProperty(ref doShare, value); }

        private string GetSeatLocation()
        {
            if (string.IsNullOrEmpty(Seat))
            {
                return "--";
            }
            if (Seat.EndsWith("A", StringComparison.InvariantCultureIgnoreCase) || Seat.EndsWith("F", StringComparison.InvariantCultureIgnoreCase))
            {
                return "window";
            }

            if (Seat.EndsWith("C", StringComparison.InvariantCultureIgnoreCase) || Seat.EndsWith("D", StringComparison.InvariantCultureIgnoreCase))
            {
                return "aisle";
            }

            return "middle";
        }
    }

    public class PassportDetails : MvxNotifyPropertyChanged
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private int _selectedPassportGenderIndex;
        public int SelectedPassportGenderIndex
        {
            get => _selectedPassportGenderIndex;
            set => SetProperty(ref _selectedPassportGenderIndex, value);
        }

        public Gender SelectedGender => (Gender)SelectedPassportGenderIndex;
        public string SelectedGenderName => SelectedGender.GetAttributeOfType<DescriptionAttribute>().Description;

        private int _birthDay;
        public int BirthDay
        {
            get => _birthDay;
            set => SetProperty(ref _birthDay, value);
        }

        private int _birthMonth;
        public int BirthMonth
        {
            get => _birthMonth;
            set => SetProperty(ref _birthMonth, value);
        }

        private int _birthYear;
        public int BirthYear
        {
            get => _birthYear;
            set => SetProperty(ref _birthYear, value);
        }

        private string _nationality;
        public string Nationality
        {
            get => _nationality;
            set => SetProperty(ref _nationality, value);
        }

        public string NationalityCode { get; set; }

        private string _passportNumber;
        public string PassportNumber
        {
            get => _passportNumber;
            set => SetProperty(ref _passportNumber, value);
        }

        private string _issuingCountry;
        public string IssuingCountry
        {
            get => _issuingCountry;
            set => SetProperty(ref _issuingCountry, value);
        }

        public string IssuingCountryCode { get; set; }

        private int _expirationDay;
        public int ExpirationDay
        {
            get => _expirationDay;
            set => SetProperty(ref _expirationDay, value);
        }

        private int _expirationMonth;
        public int ExpirationMonth
        {
            get => _expirationMonth;
            set => SetProperty(ref _expirationMonth, value);
        }

        private int _expirationYear;
        public int ExpirationYear
        {
            get => _expirationYear;
            set => SetProperty(ref _expirationYear, value);
        }

        public DateTime ExpirationDate
        {
            get
            {
                if (ExpirationYear == 0 || ExpirationMonth == 0 || ExpirationDay == 0)
                {
                    return DateTime.MinValue;
                }

                return new DateTime(ExpirationYear, ExpirationMonth, ExpirationDay);
            }
        }
    }

    public class EmergencyContact : MvxNotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public string CountryCode { get; set; }

        private string _dialingCode;
        public string DialingCode
        {
            get => _dialingCode;
            set => SetProperty(ref _dialingCode, value);
        }

        private string _contactNumber;
        public string ContactNumber
        {
            get => _contactNumber;
            set => SetProperty(ref _contactNumber, value);
        }

        public string FormattedContactNumber()
        {
            if (string.IsNullOrEmpty(DialingCode))
            {
                return string.Empty;
            }

            return $"{DialingCode.Replace("+", string.Empty)}{ContactNumber}";
        }
    }
}