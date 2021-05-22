using System.Collections.Generic;

namespace WildBeard.Orders.ApplicationServices.Responses
{
    public interface IResponse
    {
        string OperationResultMessage { get; set; }

        bool HasFailed { get; set; }

        IEnumerable<Link> Links { get; set; }
    }
}
