#region Using Directives

using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using System.Diagnostics;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Converters
{
    #region Methods

    /// <summary>
    /// Given a pipe of values, these are used in the format string expression.
    /// </summary>
    /// <code>
    /// parameter = "Hello {0}";
    /// value = "World"
    /// result = "Hello World"
    /// </code>
    public class StringFormatValueConverter : MvxFormsValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = string.Format(parameter.ToString(), value);
                return result;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("StringFormat Converter Exception: " + exception.Message);
            }
            return "FormatError";
        }
    }

    #endregion //Methods
}
