using System;

namespace Nacelle.KMA.API.Exceptions
{
    public class CheckInException : GeneralException
    {
        public CheckInException(string errorMessage) : base(errorMessage) { }
    }

    public class ExpiredSession : GeneralException
    {
        public ExpiredSession() : base(string.Empty)
        {
        }
    }
}

