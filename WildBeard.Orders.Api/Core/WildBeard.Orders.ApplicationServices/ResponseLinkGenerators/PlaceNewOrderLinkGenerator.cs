using System;
using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public class PlaceNewOrderLinkGenerator : ILinkGenerator<PlaceNewOrderResponse>
    {
        private readonly IHttpContextProvider _contextProvider;
        private readonly string _appBaseUrl;

        public PlaceNewOrderLinkGenerator(IHttpContextProvider contextProvider)
        {
            _contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            _appBaseUrl = _contextProvider.GetAppBaseUrl();
        }

        public IEnumerable<Link> GenerateLinks(PlaceNewOrderResponse response)
        {
            var links = new List<Link>
            {
                 new Link
                 {
                     Rel = "self",
                     Method = LinkMethodsConstants.Get,
                     Href = $"{_appBaseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "cancel_order",
                     Method = LinkMethodsConstants.Patch,
                     Href = $"{_appBaseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "update_order",
                     Method = LinkMethodsConstants.Update,
                     Href = $"{_appBaseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "list_orders",
                     Method = LinkMethodsConstants.Get,
                     Href = $"{_appBaseUrl}/Orders"
                 },
                 new Link
                 {
                     Rel = "get_order_lines",
                     Method = LinkMethodsConstants.Get,
                     Href = $"{_appBaseUrl}/Orders/{response.NewOrderId}/lines"
                 }
            };

            return links;
        }
    }
}
