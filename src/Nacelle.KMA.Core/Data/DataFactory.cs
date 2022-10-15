#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nacelle.Core.Helpers;
using Nacelle.KMA.Core.Models;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Data
{
    public static class DataFactory
    {
        #region Constants

        private const string DOWN_ARROW_IMAGE_FILE_NAME = "DownArrow.png";
        private const string IMAGE_BASE_64_REPLACEMENT_TAG = "<BASE64_ENCODED_IMAGE>";

        #endregion //Constants

        #region Methods

        public static FaqDataModel CreateFaqDataModel()
        {
            return ResourceHelper.GetResourceObject<FaqDataModel, App>($"Nacelle.KMA.Core.Resources.FaqData.json");
        }

        public static string CreatePrivacyPolicyHtml()
        {
            return ResourceHelper.GetStringResource<App>($"Nacelle.KMA.Core.Resources.PrivacyPolicy.html");
        }

        public static string CreateTermsConditionHtml()
        {
            return ResourceHelper.GetStringResource<App>($"Nacelle.KMA.Core.Resources.TermsConditions.html");
        }

        public static string CreateCheckInfoHtml()
        {
            string result = ResourceHelper.GetStringResource<App>($"Nacelle.KMA.Core.Resources.CheckInfo.html");
            string downArrowBase64String = DataFactory.CreateBase64StringFromResourceFile(DOWN_ARROW_IMAGE_FILE_NAME);
            result = result.Replace(IMAGE_BASE_64_REPLACEMENT_TAG, downArrowBase64String);
            return result;
        }

        public static string CreateExitRowTermsHtml()
        {
            return ResourceHelper.GetStringResource<App>($"Nacelle.KMA.Core.Resources.ExitRowTerms.html");
        }

        public static string CreateBase64StringFromResourceFile(string resourceFileName)
        {
            byte[] fileBytes = CreateBytesFromResourceFile(resourceFileName);
            string result = string.Empty;
            if (fileBytes != null || fileBytes.Length > 0)
            {
                result = Convert.ToBase64String(fileBytes);
            }
            return result;
        }

        public static byte[] CreateBytesFromResourceFile(string resourceFileName)
        {
            string resourcePath = $"Nacelle.KMA.Core.Resources.{resourceFileName}";
            byte[] result = ResourceHelper.GetByteArrayResource<App>(resourcePath);
            return result;
        }

        public static IDictionary<string, string> AirportDictionary = new Dictionary<string, string>
        {
            { "JNB", "O.R Tambo (Jo'Burg)"},
            { "CPT", "Cape Town"},
            { "HLA", "Lanseria (Jo'Burg)"},
            { "DUR", "King Shaka (Durban)"},
            { "DBN", "King Shaka (Durban)"},
            { "PE", "Port Elizabeth"},
            { "PLZ", "Port Elizabeth"},
            { "GRJ", "George"},
            { "ELS", "East London"},
            { "HRE", "Harare"},
            { "LVI", "Livingstone"},
            { "NBO", "Nairobi"},
            { "MRU", "Mauritius"},
            { "VFA", "Victoria Falls"},
            { "WDH", "Windhoek"}
        };

        public static IDictionary<string, string> CityDictionary = new Dictionary<string, string>
        {
            { "JNB", "JOHANNESBURG"},
            { "HLA", "LANSERIA"},
            { "CPT", "CAPE TOWN"},
            { "DUR", "DURBAN"},
            { "DBN", "DURBAN"},
            { "PE", "PORT ELIZABETH"},
            { "PLZ", "PORT ELIZABETH"},
            { "GRJ", "GEORGE"},
            { "ELS", "EAST LONDON"},
            { "HRE", "HARARE"},
            { "LVI", "LIVINGSTONE"},
            { "NBO", "NAIROBI"},
            { "MRU", "MAURITIUS"},
            { "VFA", "VICTORIA FALLS"},
            { "WDH", "WINDHOEK"},
        };

        public static IEnumerable<SeatItem> CreateRowSeats(int row)
        {
            var columns = new[] { "L", "A", "B", "C", "M", "D", "E", "F", "R" };
            var exitRows = new[] { 1, 2, 15, 16, 32 };
            var isExit = exitRows.Contains(row);
            var possibleRemovedSeats = new[] { "D", "E", "F" };
            return columns.Select((x, y) =>
                new SeatItem
                {
                    ColumnLetter = x,
                    Column = y,
                    Row = row,
                    IsExit = isExit,
                    IsRemoved = row == 1 && possibleRemovedSeats.Contains(x)
                });
        }

        #endregion //Methods
    }
}
