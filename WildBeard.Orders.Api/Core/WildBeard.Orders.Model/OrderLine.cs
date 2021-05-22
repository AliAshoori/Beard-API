using System;

namespace WildBeard.Orders.Model
{
    public class OrderLine : BaseEntity
    {
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Guid ProductId { get; set; }

        public virtual Order Order { get; set; }

        public Guid OrderId { get; set; }

        public OrderLineStatus Status { get; set; }

        public virtual OrderLineCancellation Cancellation { get; set; }

        public Guid? CancellationId { get; set; }
    }
}
