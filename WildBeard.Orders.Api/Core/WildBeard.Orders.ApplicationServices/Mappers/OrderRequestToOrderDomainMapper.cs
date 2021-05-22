using System;
using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices.Mappers
{
    public class OrderRequestToOrderDomainMapper : IRequestToDomainMapper<PlaceNewOrderRequest, Order>
    {
        public Order Map(PlaceNewOrderRequest request)
        {
            var creationTime = DateTime.Now;

            var order = new Order
            {
                TransactionId = request.TransactionId,
                CreatedAt = creationTime,
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                Total = request.Total,
                OrderLines = new List<OrderLine>()
            };

            if (request.BillingAddress == null)
            {
                throw new ArgumentNullException(nameof(request.BillingAddress));
            }

            if (request.DeliveryAddress == null)
            {
                throw new ArgumentNullException(nameof(request.BillingAddress));
            }

            order.BillingAddress = new OrderBillingAddress
            {
                Id = Guid.NewGuid(),
                CreatedAt = creationTime,
                Line1 = request.BillingAddress.Line1,
                Line2 = request.BillingAddress.Line2,
                City = request.BillingAddress.City,
                Country = request.BillingAddress.Country,
                PostCode = request.BillingAddress.PostCode
            };

            order.DeliveryAddress = new OrderDeliveryAddress
            {
                Id = Guid.NewGuid(),
                CreatedAt = creationTime,
                Line1 = request.DeliveryAddress.Line1,
                Line2 = request.DeliveryAddress.Line2,
                City = request.DeliveryAddress.City,
                Country = request.DeliveryAddress.Country,
                PostCode = request.DeliveryAddress.PostCode
            };

            foreach (var item in request.OrderLines)
            {
                order.OrderLines.Add(new OrderLine 
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = creationTime,
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Status = OrderLineStatus.New
                });
            }

            return order;
        }
    }
}
