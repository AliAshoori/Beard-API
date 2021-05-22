using FluentValidation;
using System;
using System.Linq;
using WildBeard.Orders.ApplicationServices.Requests;

namespace WildBeard.Orders.ApplicationServices.RequestValidators
{
    public class PlaceNewOrderRequestValidator : AbstractValidator<PlaceNewOrderRequest>
    {
        public PlaceNewOrderRequestValidator()
        {
            RuleFor(r => r.TransactionId).NotNull().NotEmpty();

            RuleFor(r => r.CustomerId).Must(c => c != Guid.Empty && c != null).WithMessage("Invalid Customer Id detected");

            RuleFor(r => r.Total).Must(t => t > 0).WithMessage("Order total amount cannot be equal or less than zero");

            RuleFor(r => r.OrderLines).Must(l => l.Any() && l.All(x => x.UnitPrice > 0) &&
                                                 l.All(x => x.Quantity > 0) &&
                                                 l.All(x => x.ProductId != Guid.Empty))
                .WithMessage("Order lines are not valid, please check all the values");

            RuleFor(r => r).Must(r => r.OrderLines.Sum(l => l.UnitPrice) == r.Total).When(x => x.Total > 0).WithMessage("The order total does not match the sum of the order lines");

            RuleFor(r => r.DeliveryAddress).Must(address => address != null &&
                                                                 !string.IsNullOrWhiteSpace(address.Line1) &&
                                                                 !string.IsNullOrWhiteSpace(address.Line2) &&
                                                                 !string.IsNullOrWhiteSpace(address.City) &&
                                                                 !string.IsNullOrWhiteSpace(address.Country) &&
                                                                 !string.IsNullOrWhiteSpace(address.PostCode))
                .WithMessage("The delivery address is not valid, please check all the values");

            RuleFor(r => r.BillingAddress).Must(address => address != null &&
                                                                 !string.IsNullOrWhiteSpace(address.Line1) &&
                                                                 !string.IsNullOrWhiteSpace(address.Line2) &&
                                                                 !string.IsNullOrWhiteSpace(address.City) &&
                                                                 !string.IsNullOrWhiteSpace(address.Country) &&
                                                                 !string.IsNullOrWhiteSpace(address.PostCode))
                .WithMessage("The billing address is not valid, please check all the values");
        }
    }
}
