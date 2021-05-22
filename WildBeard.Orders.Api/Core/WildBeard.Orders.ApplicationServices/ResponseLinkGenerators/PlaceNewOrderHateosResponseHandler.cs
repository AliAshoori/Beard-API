using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using WildBeard.Orders.ApplicationServices.Responses;
using Microsoft.Extensions.Logging;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public class PlaceNewOrderHateosResponseHandler : IHateosResponseHandler<PlaceNewOrderResponse>
    {
        private readonly ILogger<PlaceNewOrderHateosResponseHandler> _logger;
        private readonly ILinkGenerator<PlaceNewOrderResponse> _linkGenerator;
        private readonly IOptions<ApiConfigsOptions> _options;
        private readonly IHttpContextProvider _contextProvider;

        public PlaceNewOrderHateosResponseHandler(
            IOptions<ApiConfigsOptions> options,
            ILogger<PlaceNewOrderHateosResponseHandler> logger,
            ILinkGenerator<PlaceNewOrderResponse> linkGenerator,
            IHttpContextProvider contextProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
            _contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
        }

        public void AddLinksToResponseIfNeeded(PlaceNewOrderResponse response)
        {
            var mediaType = (MediaTypeHeaderValue)_contextProvider.GetCurrentContext().Items["AcceptHeaderMediaType"];
            
            if (mediaType.MediaType == _options.Value.HateosMediaType)
            {
                _logger.LogInformation("Generating links for the place new order response");
                response.Links = _linkGenerator.GenerateLinks(response);
            }
        }
    }
}
