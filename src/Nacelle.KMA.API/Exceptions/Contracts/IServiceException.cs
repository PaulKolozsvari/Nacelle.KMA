namespace Nacelle.KMA.API.Exceptions.Contracts
{
    public interface IServiceException
    {
        string Type { get; }
        string Code { get; }
    }
}
