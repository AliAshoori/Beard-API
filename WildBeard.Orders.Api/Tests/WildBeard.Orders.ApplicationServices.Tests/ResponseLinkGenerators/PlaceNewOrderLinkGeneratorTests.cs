using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.ResponseLinkGenerators;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.Tests.ResponseLinkGenerators
{
    [TestClass]
    public class PlaceNewOrderLinkGeneratorTests
    {
        [TestMethod]
        public void GenerateLinks_NormalScenario_ReturnsPlaceNewOrderLinks()
        {
            // Arrange
            const string baseUrl = "baseurl";
            var mockContextProvider = new Mock<IHttpContextProvider>();
            mockContextProvider.Setup(m => m.GetAppBaseUrl()).Returns(baseUrl);

            var response = new PlaceNewOrderResponse
            {
                HasFailed = false,
                Links = new List<Link>(),
                NewOrderId = Guid.NewGuid(),
                OperationResultMessage = "All good"
            };

            var expected = new List<Link>
                {
                 new Link
                 {
                     Rel = "self",
                     Method = "GET",
                     Href = $"{baseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "cancel_order",
                     Method = "PATCH",
                     Href = $"{baseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "update_order",
                     Method = "UPDATE",
                     Href = $"{baseUrl}/Order/{response.NewOrderId}"
                 },
                 new Link
                 {
                     Rel = "list_orders",
                     Method = "GET",
                     Href = $"{baseUrl}/Orders"
                 },
                 new Link
                 {
                     Rel = "get_order_lines",
                     Method = "GET",
                     Href = $"{baseUrl}/Orders/{response.NewOrderId}/lines"
                 }
                };

            var generator = new PlaceNewOrderLinkGenerator(mockContextProvider.Object);

            // Act
            var actual = generator.GenerateLinks(response);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GenerateLinks_WithNoContextProviderPassedToConstructor_ThrowsArgumentNullException()
        {
            // Arrange
            IHttpContextProvider httpContextProvider = null;

            // Act
            Action init = () => new PlaceNewOrderLinkGenerator(httpContextProvider);

            // Assert
            init.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
