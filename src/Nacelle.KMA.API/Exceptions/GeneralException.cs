using System;

namespace Nacelle.KMA.API.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
