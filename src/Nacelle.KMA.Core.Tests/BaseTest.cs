using MvvmCross.Tests;
using MvvmCross.Navigation;
using FakeItEasy;

namespace Nacelle.KMA.Core.Tests
{
    public class BaseTest : MvxIoCSupportingTest
    {
        public IMvxNavigationService Navigationservice { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Navigationservice = A.Fake<IMvxNavigationService>();
        }
    }
}
