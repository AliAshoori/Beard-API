using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WildBeard.Orders.ApplicationServices.Responses;

namespace WildBeard.Orders.ApplicationServices.Requests
{
    public class PlaceNewOrderRequest : IRequest<PlaceNewOrderResponse>
    {
        [JsonConstructor]
        public PlaceNewOrderRequest(decimal total, Guid customerId, Address deliveryAddress, Address billingAddress, string transactionId, IEnumerable<OrderLine> orderLines)
        {
            Total = total;
            CustomerId = customerId;
            DeliveryAddress = deliveryAddress;
            BillingAddress = billingAddress;
            OrderLines = orderLines;
            TransactionId = transactionId;
        }

        public decimal Total { get; }

        public Guid CustomerId { get; }

        public string TransactionId { get; }

        public Address DeliveryAddress { get; }

        public Address BillingAddress { get; }

        public IEnumerable<OrderLine> OrderLines { get; }

        public class Address
        {
            [JsonConstructor]
            public Address(string line1, string line2, string city, string country, string postcode)
            {
                Line1 = line1;
                Line2 = line2;
                City = city;
                Country = country;
                PostCode = postcode;
            }

            public string Line1 { get; }

            public string Line2 { get; }

            public string City { get; }

            public string Country { get; }

            public string PostCode { get; }
        }

        public class OrderLine
        {
            [JsonConstructor]
            public OrderLine(decimal unitPrice, int quantity, Guid productId)
            {
                UnitPrice = unitPrice;
                Quantity = quantity;
                ProductId = productId;
            }

            public decimal UnitPrice { get; }

            public int Quantity { get; }

            public Guid ProductId { get; }
        }
    }
}