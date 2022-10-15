#region Using Directives

using System;

#endregion //Using Directives

namespace Nacelle.Core.Exceptions
{
    public class KululaNetworkException : Exception
    {
        #region Constructors

        public KululaNetworkException(Exception innerException) : base(DEFAULT_MESSAGE, innerException)
        {
        }

        public KululaNetworkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion //Constructors

        #region Constants

        private const string DEFAULT_MESSAGE = "No response received from kulula.com, please try again.";

        #endregion //Constants
    }
}