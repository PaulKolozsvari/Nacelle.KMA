using Nacelle.KMA.API.Exceptions.Contracts;

namespace Nacelle.KMA.API.Exceptions
{
    public class FindBookingException : GeneralException, IServiceException
    {
        public FindBookingException(string errorMessage, string code, string type) : base(errorMessage)
        {
            Code = code;
            Type = type;
        }

        public FindBookingException(string errorMessage) : base(errorMessage)
        {
        }

        public string Type { get; }
        public string Code { get; }
    }
}
        