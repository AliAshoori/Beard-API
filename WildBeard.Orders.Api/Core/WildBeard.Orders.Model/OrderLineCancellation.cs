using System.Collections.Generic;

namespace WildBeard.Orders.Model
{
    public class OrderLineCancellation : BaseEntity
    {
        public CancellationReason Reason { get; set; }

        public string MoreInfo { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}