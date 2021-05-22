using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using WildBeard.Orders.ApplicationServices.Mappers;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.ApplicationServices.Responses;
using WildBeard.Orders.InfraServices.RepositoryServices.Contracts;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices.RequestHandlers
{
    public class PlaceNewOrderRequestHandler : IRequestHandler<PlaceNewOrderRequest, PlaceNewOrderResponse>
    {
        private readonly IPlaceNewOrderRepository _repository;
        private readonly IRequestToDomainMapper<PlaceNewOrderRequest, Order> _mapper;
        private readonly ILogger<PlaceNewOrderRequestHandler> _logger;

        public PlaceNewOrderRequestHandler(
            IPlaceNewOrderRepository repository, 
            IRequestToDomainMapper<PlaceNewOrderRequest, Order> mapper,
            ILogger<PlaceNewOrderRequestHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PlaceNewOrderResponse> Handle(PlaceNewOrderRequest request, CancellationToken cancellationToken)
        {
            var response = new PlaceNewOrderResponse
            {
                HasFailed = false,
                OperationResultMessage = "Successfully created new order"
            };

            try
            {
                response.NewOrderId = await _repository.PlaceNewOrderAsync(_mapper.Map(request));
                
                _logger.LogInformation(JsonConvert.SerializeObject(response));
            }
            catch (Exception exception)
            {
                response.HasFailed = true;
                response.OperationResultMessage = "Placing new order failed. Please try again later";

                _logger.LogError($"{exception.Message}"); // TODO: flatten the message
            }

            return response;
        }
    }
}
