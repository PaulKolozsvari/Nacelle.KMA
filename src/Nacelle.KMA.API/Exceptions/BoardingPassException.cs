using Nacelle.KMA.API.Exceptions.Contracts;
namespace Nacelle.KMA.API.Exceptions
{
    public class BoardingPassException : GeneralException, IServiceException
    {
        public string Type { get; }

        public string Code { get; }

        public BoardingPassException(string errorMessage) : base(errorMessage) { }

        public BoardingPassException(string errorMessage, string code, string type) : base(errorMessage)
        {
            Code = code;
            Type = type;
        }
    }
}

