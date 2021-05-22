using System;

namespace WildBeard.Orders.Model
{
    public class OrderDeliveryAddress : BaseAddress
    {
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
