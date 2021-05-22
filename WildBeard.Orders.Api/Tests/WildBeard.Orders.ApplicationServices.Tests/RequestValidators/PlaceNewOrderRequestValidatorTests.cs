using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.ApplicationServices.RequestValidators;

namespace WildBeard.Orders.ApplicationServices.Tests
{
    [TestClass]
    public class PlaceNewOrderRequestValidatorTests
    {
        [TestMethod]
        public void Validate_NormalScenario_PassesAllRules()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(0);
        }

        [TestMethod]
        public void Validate_WithZeroTotalAmount_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                0,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("Order total amount cannot be equal or less than zero");
        }

        [TestMethod]
        public void Validate_WithTotalAmountNotMatchedOrderLinesSum_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                100,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The order total does not match the sum of the order lines");
        }

        [TestMethod]
        public void Validate_WithInvalidCustomerId_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.Empty,
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("Invalid Customer Id detected");
        }

        // -- order lines

        [TestMethod]
        public void Validate_WithNoOrderLines_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                Enumerable.Empty<PlaceNewOrderRequest.OrderLine>());

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors.Any(error => error.ErrorMessage == "The order total does not match the sum of the order lines").Should().BeTrue();
            result.Errors.Any(error => error.ErrorMessage == "Order lines are not valid, please check all the values").Should().BeTrue();
        }

        [TestMethod]
        public void Validate_WithOrderLinesHavingInvalidUnitPrice_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(0m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors.Any(error => error.ErrorMessage == "The order total does not match the sum of the order lines").Should().BeTrue();
            result.Errors.Any(error => error.ErrorMessage == "Order lines are not valid, please check all the values").Should().BeTrue();
        }

        [TestMethod]
        public void Validate_WithOrderLinesHavingInvalidQuantity_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 0, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors.Any(error => error.ErrorMessage == "Order lines are not valid, please check all the values").Should().BeTrue();
        }

        // -- delivery address

        [TestMethod]
        public void Validate_WithDeliveryAddressHavingNoLine1_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithDeliveryAddressHavingNoLine2_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithDeliveryAddressHavingNoCity_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithDeliveryAddressHavingNoCountry_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithDeliveryAddressHavingPostCode_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", ""),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithNoAddress_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                null,
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The delivery address is not valid, please check all the values");
        }

        // -- billing address

        [TestMethod]
        public void Validate_WithBillingAddressHavingNoLine1_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("", "line2", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithBillingAddressHavingNoLine2_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "", "city", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithBiilingAddressHavingNoCity_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "", "country", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithBillingAddressHavingNoCountry_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "", "postcode"),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithBillingAddressHavingPostCode_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", ""),
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        [TestMethod]
        public void Validate_WithNoBillingAddress_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                null,
                "T1",
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("The billing address is not valid, please check all the values");
        }

        // -- transaction id 

        [TestMethod]
        public void Validate_WithNoTransactionId_FailsValidation()
        {
            // Arrange
            var request = new PlaceNewOrderRequest(
                10.23m,
                Guid.NewGuid(),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                new PlaceNewOrderRequest.Address("line1", "line2", "city", "country", "postcode"),
                string.Empty,
                new List<PlaceNewOrderRequest.OrderLine> { new PlaceNewOrderRequest.OrderLine(10.23m, 1, Guid.NewGuid()) });

            var validator = new PlaceNewOrderRequestValidator();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be("'Transaction Id' must not be empty.");
        }
    }
}
