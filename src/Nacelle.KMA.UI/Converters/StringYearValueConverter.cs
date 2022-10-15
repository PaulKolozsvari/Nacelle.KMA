#region Using Directives

using System;
using System.Diagnostics;
using System.Globalization;
using MvvmCross.Forms.Converters;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Converters
{
    /// <summary>
    /// Given a pipe of values, these are used in the format string expression.
    /// </summary>
    /// <code>
    /// parameter = "Hello {0}";
    /// value = "World"
    /// result = "Hello World"
    /// </code>
    public class StringYearValueConverter : MvxFormsValueConverter
    {
        #region Methods

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is DateTime dt && dt.Year == DateTime.Now.Year)
                {
                    return string.Empty;
                }
                var result = string.Format(parameter.ToString(), value);
                return result;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("StringFormat Converter Exception: " + exception.Message);
            }

            return "FormatError";
        }

        #endregion //Methods
    }
}
