using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WildBeard.Orders.ApplicationServices.Mappers;
using WildBeard.Orders.ApplicationServices.RequestHandlers;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.ApplicationServices.Responses;
using WildBeard.Orders.InfraServices.RepositoryServices.Contracts;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices.Tests.RequestHandlers
{
    [TestClass]
    public class PlaceNewOrderRequestHandlerTests
    {
        [TestMethod]
        public async Task PlaceNewOrderRequestHandler_NormalScenario_ReturnsResponse()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                            10.23m,
                            Guid.NewGuid(),
                            new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                            new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                            "T1",
                            new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var expected = new PlaceNewOrderResponse
            {
                NewOrderId = Guid.NewGuid(),
                HasFailed = false,
                OperationResultMessage = "Successfully created new order"
            };

            var mockMapper = new Mock<IRequestToDomainMapper<PlaceNewOrderRequest, Order>>();
            mockMapper.Setup(m => m.Map(request)).Returns(It.IsAny<Order>());

            var mockLogger = new Mock<ILogger<PlaceNewOrderRequestHandler>>();

            var mockRepo = new Mock<IPlaceNewOrderRepository>();
            mockRepo.Setup(m => m.PlaceNewOrderAsync(It.IsAny<Order>())).ReturnsAsync(expected.NewOrderId);

            var handler = new PlaceNewOrderRequestHandler(mockRepo.Object, mockMapper.Object, mockLogger.Object);

            // Act
            var actual = await handler.Handle(request, default);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task PlaceNewOrderRequestHandler_WithRepoServiceThrowsException_ReturnsFailureResponse()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                            10.23m,
                            Guid.NewGuid(),
                            new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                            new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                            "T1",
                            new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var expected = new PlaceNewOrderResponse
            {
                NewOrderId = Guid.Empty,
                HasFailed = true,
                OperationResultMessage = "Placing new order failed. Please try again later"
            };

            var mockMapper = new Mock<IRequestToDomainMapper<PlaceNewOrderRequest, Order>>();
            mockMapper.Setup(m => m.Map(request)).Returns(It.IsAny<Order>());

            var mockLogger = new Mock<ILogger<PlaceNewOrderRequestHandler>>();

            var mockRepo = new Mock<IPlaceNewOrderRepository>();
            mockRepo.Setup(m => m.PlaceNewOrderAsync(It.IsAny<Order>())).ThrowsAsync(new Exception());

            var handler = new PlaceNewOrderRequestHandler(mockRepo.Object, mockMapper.Object, mockLogger.Object);

            // Act
            var actual = await handler.Handle(request, default);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void PlaceNewOrderRequestHandler_WithNoLoggerPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            var mockMapper = new Mock<IRequestToDomainMapper<PlaceNewOrderRequest, Order>>();
            ILogger<PlaceNewOrderRequestHandler> logger = null;
            var mockRepo = new Mock<IPlaceNewOrderRepository>();

            // Act
            Action init = () => new PlaceNewOrderRequestHandler(mockRepo.Object, mockMapper.Object, logger);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void PlaceNewOrderRequestHandler_WithNoMapperPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            IRequestToDomainMapper<PlaceNewOrderRequest, Order> mapper = null;
            var mockLogger = new Mock<ILogger<PlaceNewOrderRequestHandler>>();
            var mockRepo = new Mock<IPlaceNewOrderRepository>();

            // Act
            Action init = () => new PlaceNewOrderRequestHandler(mockRepo.Object, mapper, mockLogger.Object);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void PlaceNewOrderRequestHandler_WithNoRepoServicePassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            var mockMapper = new Mock<IRequestToDomainMapper<PlaceNewOrderRequest, Order>>();
            var mockLogger = new Mock<ILogger<PlaceNewOrderRequestHandler>>();
            IPlaceNewOrderRepository repo = null;

            // Act
            Action init = () => new PlaceNewOrderRequestHandler(repo, mockMapper.Object, mockLogger.Object);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
