using System;

namespace WildBeard.Orders.Model
{
    public class OrderBillingAddress : BaseAddress
    {
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
