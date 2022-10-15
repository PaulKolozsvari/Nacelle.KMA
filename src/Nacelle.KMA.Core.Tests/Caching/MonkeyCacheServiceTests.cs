using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nacelle.KMA.Core.Caching;
using FluentAssertions;
using MonkeyCache.FileStore;
using MonkeyCache;
using Nacelle.KMA.Core.Tests.Models;
using Nacelle.KMA.Core.Tests.Helpers;

namespace Nacelle.KMA.Core.Tests.Caching
{
    [TestClass]
    public class MonkeyCacheServiceTests : BaseTest
    {
        private MonkeyCacheService _cacheService;
        private static bool _initialized;

        public MonkeyCacheServiceTests()
        {
            if (!_initialized)
            {
                BarrelUtils.SetBaseCachePath(Environment.CurrentDirectory);
                _initialized = true;
            }
        }

        [TestInitialize]
        public void Intialize()
        {
            Barrel.ApplicationId = "com.nacelle.kulula.TestFileStore";
            _cacheService = new MonkeyCacheService(Barrel.Current);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _cacheService.ClearAll();
            _cacheService = null;
        }

        [TestMethod]
        public void Test_Cache_SetValue_And_GetValue_Successfully()
        {
            //Arrange
            var key = "UserKey";
            var itemToSave = new CacheBuilder().WithDefaults().Build();

            //Act
            _cacheService.SetValue<Cache>(key, itemToSave);
            var cache = _cacheService.GetValue<Cache>(key);

            //Assert
            cache.Should().NotBeNull();
            cache.Should().BeEquivalentTo(itemToSave);
        }

        [TestMethod]
        public void Test_Cache_SetValue_And_GetValue_Twice_Successfully()
        {
            //Arrange
            var key = "UserKey";
            var itemToSave = new CacheBuilder().WithDefaults().Build();

            var overrideItemToSave = new CacheBuilder().WithItem1("OverridenValue1").WithItem2("OverridenValue2").Build();

            //Act
            _cacheService.SetValue<Cache>(key, itemToSave);
            _cacheService.SetValue<Cache>(key, overrideItemToSave);

            var cache = _cacheService.GetValue<Cache>(key);

            //Assert
            cache.Should().NotBeNull();
            cache.Should().BeEquivalentTo(overrideItemToSave);
        }

        [TestMethod]
        public void Test_Cache_Clear_By_Key_Successfully()
        {
            //Arrange
            var key = "UserKey";

            var itemToSave = new CacheBuilder().WithDefaults().Build();

            //Act
            _cacheService.SetValue<Cache>(key, itemToSave);

            _cacheService.Clear(key);

            var cache = _cacheService.GetValue<Cache>(key);

            //Assert
            cache.Should().BeNull();
        }

        [TestMethod]
        public void Test_Cache_Expire_Successfully()
        {
            //Arrange
            var key = "UserKey";

            var itemToSave = new CacheBuilder().WithDefaults().Build();

            //Act
            _cacheService.SetValue<Cache>(key, itemToSave, TimeSpan.Zero);

            var cache = _cacheService.GetValue<Cache>(key);

            //Assert
            cache.Should().BeNull();
        }
    }
}
