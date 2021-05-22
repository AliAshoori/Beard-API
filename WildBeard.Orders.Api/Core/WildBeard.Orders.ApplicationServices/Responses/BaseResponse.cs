using System.Collections.Generic;

namespace WildBeard.Orders.ApplicationServices.Responses
{
    public abstract class BaseResponse : IResponse
    {
        public string OperationResultMessage { get; set; }

        public bool HasFailed { get; set; }

        public IEnumerable<Link> Links { get; set; } = new List<Link>();
    }
}
