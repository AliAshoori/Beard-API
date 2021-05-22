using System;
using System.Collections.Generic;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices.Responses
{
    public class GetOrderResponse
    {
        public string TransactionId { get; set; }

        public decimal Total { get; set; }

        public Uri CustomerUrl { get; set; }

        public Guid CustomerId { get; set; }

        public Guid OrderDeliveryAddressId { get; set; }

        public Uri OrderDeliveryAddressUrl { get; set; }

        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}
