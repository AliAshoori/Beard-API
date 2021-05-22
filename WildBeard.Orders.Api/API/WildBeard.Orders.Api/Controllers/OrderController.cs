using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.ApplicationServices.ResponseLinkGenerators;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ApplicationBaseController<OrderController>
    {
        private readonly IHateosResponseHandler<PlaceNewOrderResponse> _hateosHandler;

        public OrderController(
            ILogger<OrderController> logger,
            IMediator mediator,
            IHttpContextProvider contextProvider,
            IHateosResponseHandler<PlaceNewOrderResponse> hateosHandler) : base(logger, mediator, contextProvider)
        {
            _hateosHandler = hateosHandler;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderAsync(Guid id)
        {
            return Ok($"order of {id}");
        }

        [HttpPost]
        public async Task<IActionResult> PlaceNewOrderAsync(PlaceNewOrderRequest request)
        {
            _logger.LogInformation("A new order request received");

            var response = await _mediator.Send(request);

            if (response.HasFailed)
            {
                return Problem($"Message: {response.OperationResultMessage}, Execution time: {DateTime.Now}");
            }

            _hateosHandler.AddLinksToResponseIfNeeded(response);

            return Created(new Uri($"{_contextProvider.GetAppBaseUrl()}/Order/{response.NewOrderId}", UriKind.Absolute), response);
        }
    }
}
