using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.Mappers;
using WildBeard.Orders.ApplicationServices.Requests;

namespace WildBeard.Orders.ApplicationServices.Tests.Mappers
{
    [TestClass]
    public class OrderRequestToOrderDomainMapperTests
    {
        [TestMethod]
        public void Map_NormalScanario_ReturnsOrderObject()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
               10.23m,
               Guid.NewGuid(),
               new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
               new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
               "T1",
               new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var mapper = new OrderRequestToOrderDomainMapper();

            // Act
            var actual = mapper.Map(request);

            // Assert
            actual.DeliveryAddress.Line1.Should().BeEquivalentTo(request.DeliveryAddress.Line1);
            actual.DeliveryAddress.Line2.Should().BeEquivalentTo(request.DeliveryAddress.Line2);
            actual.DeliveryAddress.City.Should().BeEquivalentTo(request.DeliveryAddress.City);
            actual.DeliveryAddress.Country.Should().BeEquivalentTo(request.DeliveryAddress.Country);

            actual.BillingAddress.Line1.Should().BeEquivalentTo(request.BillingAddress.Line1);
            actual.BillingAddress.Line2.Should().BeEquivalentTo(request.BillingAddress.Line2);
            actual.BillingAddress.City.Should().BeEquivalentTo(request.BillingAddress.City);
            actual.BillingAddress.Country.Should().BeEquivalentTo(request.BillingAddress.Country);

            actual.TransactionId.Should().BeEquivalentTo(request.TransactionId);
            actual.Total.Should().Be(request.Total);
        }

        [TestMethod]
        public void Map_WithNoDeliveryAddress_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
               10.23m,
               Guid.NewGuid(),
               null,
               new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
               "T1",
               new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var mapper = new OrderRequestToOrderDomainMapper();

            // Act
            Action actual = () => mapper.Map(request);

            // Assert
            actual.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void Map_WithNoBillingAddress_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
               10.23m,
               Guid.NewGuid(),
               new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
               null,
               "T1",
               new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var mapper = new OrderRequestToOrderDomainMapper();

            // Act
            Action actual = () => mapper.Map(request);

            // Assert
            actual.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
