namespace Nacelle.KMA.API.Exceptions
{
    public class BookingException : GeneralException
    {
        public BookingException(string errorMessage) : base(errorMessage) { }
    }
}
