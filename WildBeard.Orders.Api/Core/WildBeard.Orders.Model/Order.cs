using System;
using System.Collections.Generic;

namespace WildBeard.Orders.Model
{
    public class Order : BaseEntity
    {
        public string TransactionId { get; set; }

        public decimal Total { get; set; }

        public Guid CustomerId { get; set; }

        public virtual OrderDeliveryAddress DeliveryAddress { get; set; }

        public virtual OrderBillingAddress BillingAddress { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}