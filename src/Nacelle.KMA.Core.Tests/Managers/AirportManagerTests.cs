using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Tests
{
    [TestClass]
    public class AirportManagerTests : BaseTest
    {
        private IAirportManager _airportManager;
        private IOpsApiService _apiService;
        private ICacheService _cacheService;

        [TestInitialize]
        public void Initialize()
        {
            _apiService = A.Fake<IOpsApiService>();
            _cacheService = A.Fake<ICacheService>();
            var connectivityManager = A.Fake<IConnectivityManager>();
            _airportManager = new AirportManager(_apiService, _cacheService, connectivityManager);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _airportManager = null;
        }

        [TestMethod]
        public async Task Test_GetAirports_Successfully()
        {
            //Arrange
            base.AdditionalSetup();

            var response = ResponseGenerator<AirportResponse>.Success(new AirportResponse
            {
                Airports = new List<Airport>
                {
                    new Airport
                    {
                        Code = "JNB",
                        Name = "O.R. Tambo (Jo'burg)",
                        CountrCode = "ZA",
                        CountryName = "South Africa"                       
                    }
                }
            });

            A.CallTo(() => _apiService.GetAirportsAsync()).Returns(response);
            A.CallTo(() => _cacheService.GetOrUpdateValue<AirportResponse>(A<string>.Ignored, A<Func<Task<AirportResponse>>>.Ignored, A<int>.Ignored, A<bool>.Ignored)).Returns(response.Data);

            //Act 
            var airportEntity = await _airportManager.GetAirport("JNB");

            //Assert
            airportEntity.Should().BeOfType<AirportEntity>();
            airportEntity.Should().BeEquivalentTo(response.Data.Airports.FirstOrDefault());
        }

        [TestMethod]
        public async Task Test_GetAirports_With_Empty_AirportCode_Returns_Null()
        {
            //Arrange
            var airportCode = string.Empty;

            //Act
            var airportEntity = await _airportManager.GetAirport(airportCode);

            //Assert
            airportEntity.Should().BeNull();
        }
    }
}
