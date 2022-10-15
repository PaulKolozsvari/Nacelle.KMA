using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nacelle.KMA.API.Models.Enums;
using Nacelle.KMA.Core.ExtensionMethods;

namespace Nacelle.KMA.Core.Tests.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void TestParseEnumFromUpperCase()
        {
            var enumStringValue = "CHILD";
            var result = enumStringValue.PassengerTypeFromString();
            Assert.AreEqual(PassengerTypes.Child, result);
        }

        [TestMethod]
        public void TestParseEnumFromLowerCase()
        {
            var enumStringValue = "INFANT";
            var result = enumStringValue.PassengerTypeFromString();
            Assert.AreEqual(PassengerTypes.Infant, result);
        }

        [TestMethod]
        public void TestParseEnumFromStringInValidValue()
        {
            var enumStringValue = "BLAH";
            var result = enumStringValue.PassengerTypeFromString();
            Assert.AreEqual(PassengerTypes.Unknown, result);
        }

        [TestMethod]
        public void TestParseEnumFromStringNullValue()
        {
            string enumStringValue = null;
            var result = enumStringValue.PassengerTypeFromString();
            Assert.AreEqual(PassengerTypes.Unknown, result);
        }
    }
}
