using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public interface ILinkGenerator<TResponse> where TResponse : IResponse
    {
        IEnumerable<Link> GenerateLinks(TResponse response);
    }
}
