using System;
using System.Collections.Generic;

namespace WildBeard.Orders.ApplicationServices.Responses
{
    public class PlaceNewOrderResponse : BaseResponse
    {
        public Guid NewOrderId { get; set; }
    }
}
