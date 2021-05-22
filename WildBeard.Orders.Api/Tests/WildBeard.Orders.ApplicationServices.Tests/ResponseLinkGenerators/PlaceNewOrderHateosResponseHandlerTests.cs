using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WildBeard.Orders.ApplicationServices.ResponseLinkGenerators;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.Tests.ResponseLinkGenerators
{
    [TestClass]
    public class PlaceNewOrderHateosResponseHandlerTests
    {
        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithHateosMediaType_AddsLinksToResponse()
        {
            // Arrange
            var response = new PlaceNewOrderResponse
            {
                HasFailed = false,
                Links = new List<Link>(),
                NewOrderId = Guid.NewGuid(),
                OperationResultMessage = "All good"
            };

            var links = new List<Link> { new Link { Href = "href", Method = "method", Rel = "rel" } };

            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(m => m.Items).Returns(new Dictionary<object, object> { { "AcceptHeaderMediaType", new System.Net.Http.Headers.MediaTypeHeaderValue("application/hateos+json") } });

            var mockHttpContextProvider = new Mock<IHttpContextProvider>();
            mockHttpContextProvider.Setup(m => m.GetCurrentContext()).Returns(mockContext.Object);

            var mockLogger = new Mock<ILogger<PlaceNewOrderHateosResponseHandler>>();

            var mockLinkGenerator = new Mock<ILinkGenerator<PlaceNewOrderResponse>>();

            mockLinkGenerator.Setup(m => m.GenerateLinks(response)).Returns(links);

            var mockOptions = new Mock<IOptions<ApiConfigsOptions>>();
            mockOptions.Setup(m => m.Value).Returns(new ApiConfigsOptions { HateosMediaType = "application/hateos+json" });

            var handler = new PlaceNewOrderHateosResponseHandler(mockOptions.Object, mockLogger.Object, mockLinkGenerator.Object, mockHttpContextProvider.Object);

            // Act
            handler.AddLinksToResponseIfNeeded(response);

            // Assert
            response.Links.Should().BeEquivalentTo(links);
            mockLinkGenerator.Verify(m => m.GenerateLinks(response), Times.Exactly(1));

        }

        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithNoHateosMediaType_AddsNoLinksToResponse()
        {
            // Arrange
            var response = new PlaceNewOrderResponse
            {
                HasFailed = false,
                Links = new List<Link>(),
                NewOrderId = Guid.NewGuid(),
                OperationResultMessage = "All good"
            };

            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(m => m.Items).Returns(new Dictionary<object, object> { { "AcceptHeaderMediaType", new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") } });

            var mockHttpContextProvider = new Mock<IHttpContextProvider>();
            mockHttpContextProvider.Setup(m => m.GetCurrentContext()).Returns(mockContext.Object);

            var mockLogger = new Mock<ILogger<PlaceNewOrderHateosResponseHandler>>();

            var mockLinkGenerator = new Mock<ILinkGenerator<PlaceNewOrderResponse>>();

            var mockOptions = new Mock<IOptions<ApiConfigsOptions>>();
            mockOptions.Setup(m => m.Value).Returns(new ApiConfigsOptions { HateosMediaType = "application/hateos+json" });

            var handler = new PlaceNewOrderHateosResponseHandler(mockOptions.Object, mockLogger.Object, mockLinkGenerator.Object, mockHttpContextProvider.Object);

            // Act
            handler.AddLinksToResponseIfNeeded(response);

            // Assert
            response.Links.Should().BeEquivalentTo(Enumerable.Empty<Link>());
            mockLinkGenerator.Verify(m => m.GenerateLinks(response), Times.Never);
        }

        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithNoLinkGeneratorPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpContextProvider = new Mock<IHttpContextProvider>();
            var mockLogger = new Mock<ILogger<PlaceNewOrderHateosResponseHandler>>();
            ILinkGenerator<PlaceNewOrderResponse> linkGenerator = null;
            var mockOptions = new Mock<IOptions<ApiConfigsOptions>>();

            // Act
            Action init = () => new PlaceNewOrderHateosResponseHandler(mockOptions.Object, mockLogger.Object, linkGenerator, mockHttpContextProvider.Object);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithNoLoggerPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpContextProvider = new Mock<IHttpContextProvider>();
            ILogger<PlaceNewOrderHateosResponseHandler> logger = null;
            var mockLinkGenerator = new Mock<ILinkGenerator<PlaceNewOrderResponse>>();
            var mockOptions = new Mock<IOptions<ApiConfigsOptions>>();

            // Act
            Action init = () => new PlaceNewOrderHateosResponseHandler(mockOptions.Object, logger, mockLinkGenerator.Object, mockHttpContextProvider.Object);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithNoHttpContextProviderPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            IHttpContextProvider contextProvider = null;
            var mockLogger = new Mock<ILogger<PlaceNewOrderHateosResponseHandler>>();
            var mockLinkGenerator = new Mock<ILinkGenerator<PlaceNewOrderResponse>>();
            var mockOptions = new Mock<IOptions<ApiConfigsOptions>>();

            // Act
            Action init = () => new PlaceNewOrderHateosResponseHandler(mockOptions.Object, mockLogger.Object, mockLinkGenerator.Object, contextProvider);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void PlaceNewOrderHateosResponseHandler_WithNoOptionsPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpContextProvider = new Mock<IHttpContextProvider>();
            var mockLogger = new Mock<ILogger<PlaceNewOrderHateosResponseHandler>>();
            var mockLinkGenerator = new Mock<ILinkGenerator<PlaceNewOrderResponse>>();
            IOptions<ApiConfigsOptions> options = null;

            // Act
            Action init = () => new PlaceNewOrderHateosResponseHandler(options, mockLogger.Object, mockLinkGenerator.Object, mockHttpContextProvider.Object);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
