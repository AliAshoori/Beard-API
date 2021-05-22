using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WildBeard.Orders.ApplicationServices.ResponseLinkGenerators;

namespace WildBeard.Orders.Api.Controllers
{
    public abstract class ApplicationBaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;
        protected readonly IMediator _mediator;
        protected readonly IHttpContextProvider _contextProvider;

        protected ApplicationBaseController(
            ILogger<TController> logger,
            IMediator mediator,
            IHttpContextProvider contextProvider)
        {
            _logger = logger;
            _mediator = mediator;
            _contextProvider = contextProvider;
        }
    }
}
