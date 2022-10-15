using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;
using System.Collections.Generic;
using Nacelle.KMA.Core.Caching;
using FluentAssertions;
using Nacelle.KMA.Core.Models.Entites;
using System.Linq;
using System;
using System.Globalization;
using Nacelle.KMA.API.Services;

namespace Nacelle.KMA.Core.Tests
{
    [TestClass]
    public class WeatherManagerTests : BaseTest
    {
        private IWeatherManager _weatherManager;
        private IOpsApiService _apiService;
        private ICacheService _cacheService;

        [TestInitialize]
        public void Initialize()
        {
            _apiService = A.Fake<IOpsApiService>();
            _cacheService = A.Fake<ICacheService>();
            _weatherManager = new WeatherManager(_apiService, _cacheService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _weatherManager = null;
        }

        [TestMethod]
        public async Task Test_GetWeatherData_Successfully()
        {
            //Arrange
            base.AdditionalSetup();

            var response = ResponseGenerator<WeatherResponse>.Success(new WeatherResponse
            {
                AirportCode = "CPT",
                Days = new List<Days>
                {
                    new Days
                    {
                        DateTime = DateTime.ParseExact("2019-06-02T12:00:00", "yyyy-MM-ddThh:mm:ss", CultureInfo.InvariantCulture),
                        CloudPercentage = 59,
                        WindSpeed = 2.63,
                        WindDirection = 160.834,
                        Temperature = 17.9,
                        MinTemperature = 16.42,
                        MaxTemperature = 17.9,
                        RainVolume = 0.0,
                        Pressure = 1022.86,
                        SeaLevelPressure = 1022.86,
                        GroundLevelPressure = 1023.1,
                        HumidityPercentage = 84,
                        Main = "Clouds",
                        Description = "Description",
                        Icon = "http://openweathermap.org/img/w/04d.png"
                    }
                }
            });

            A.CallTo(() => _cacheService.GetOrUpdateValue<WeatherResponse>(A<string>.Ignored, A<Func<Task<WeatherResponse>>>.Ignored, A<int>.Ignored, A<bool>.Ignored)).Returns(response.Data);

            //Act 
            var weatherEntity = await _weatherManager.GetOrFetchWeatherData("CPT");

            //Assert
            weatherEntity.Should().BeOfType<List<WeatherEntity>>().And.NotBeNullOrEmpty();
            weatherEntity.Should().HaveCount(1);
            weatherEntity.FirstOrDefault().Should().BeEquivalentTo(response.Data.Days.FirstOrDefault());
        }

        [TestMethod]
        public async Task Test_FetchWeatherData_Successfully()
        {
            //Arrange
            base.AdditionalSetup();

            var response = ResponseGenerator<WeatherResponse>.Success(new WeatherResponse
            {
                AirportCode = "CPT",
                Days = new List<Days>
                {
                    new Days
                    {
                        DateTime = DateTime.ParseExact("2019-06-02T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                        CloudPercentage = 59,
                        WindSpeed = 2.63,
                        WindDirection = 160.834,
                        Temperature = 17.9,
                        MinTemperature = 16.42,
                        MaxTemperature = 17.9,
                        RainVolume = 0.0,
                        Pressure = 1022.86,
                        SeaLevelPressure = 1022.86,
                        GroundLevelPressure = 1023.1,
                        HumidityPercentage = 84,
                        Main = "Clouds",
                        Description = "Description",
                        Icon = "http://openweathermap.org/img/w/04d.png"
                    }
                }
            });

            A.CallTo(() => _apiService.GetFiveDayWeatherForcastAsync(A<WeatherRequest>.Ignored)).Returns(response);
            A.CallTo(() => _cacheService.GetOrUpdateValue<WeatherResponse>(A<string>.Ignored, A<Func<Task<WeatherResponse>>>.Ignored, A<int>.Ignored, A<bool>.Ignored)).Returns(response.Data);

            //Act 
            var weatherEntity = await _weatherManager.GetOrFetchWeatherData("CPT");

            //Assert
            weatherEntity.Should().BeOfType<List<WeatherEntity>>().And.NotBeNullOrEmpty();
            weatherEntity.Should().HaveCount(1);
            weatherEntity.FirstOrDefault().Should().BeEquivalentTo(response.Data.Days.FirstOrDefault());
        }

        [TestMethod]
        public async Task Test_GetOrFetchWeatherData_With_Empty_AirportCode_Returns_Null()
        {
            //Arrange
            var airportCode = string.Empty;

            //Act
            var weatherEntity = await _weatherManager.GetOrFetchWeatherData(airportCode);

            //Assert
            weatherEntity.Should().BeNullOrEmpty();
        }
    }
}
