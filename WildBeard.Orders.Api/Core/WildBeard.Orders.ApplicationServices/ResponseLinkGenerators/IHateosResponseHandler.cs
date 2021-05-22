using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public interface IHateosResponseHandler<TResponse> where TResponse : IResponse
    {
        void AddLinksToResponseIfNeeded(TResponse response);
    }
}
