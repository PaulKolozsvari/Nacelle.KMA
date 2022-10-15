using System;


        // TODO :: see if there is a way to DRY up the below logic

        #region EMERGENCY CONTACT
        private async void EmergencyContactCountrySelectorTapped(object sender, EventArgs e)

        private async void EmergencyContactCodeSelectorTapped(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs)
            {
                _selected = tappedEventArgs.Parameter;
            }

            var countrySelector = new CountrySelectorPopup(ViewModel.CountryDialCodes);
            countrySelector.CountrySelected += CountrySelector_EmergencyCodeCountrySelected;

            await PopupNavigation.Instance.PushAsync(countrySelector);
        }

        private void CountrySelector_EmergencyCodeCountrySelected(object sender, object e)
        {
            if (sender is CountrySelectorPopup countrySelector)
            {
                countrySelector.CountrySelected -= CountrySelector_EmergencyCodeCountrySelected;

                if (e is CountryEntity countryEntity)
                {
                    if (_selected is EmergencyContact emergencyContact)
                    {
                        emergencyContact.CountryCode = countryEntity.Code;
                    }
                }
            }
        }



        #endregion
        #region Passport
        private async void PassportNationalityCountrySelectorTapped(object sender, EventArgs e)


        #endregion
        private readonly List<int> _monthDays;