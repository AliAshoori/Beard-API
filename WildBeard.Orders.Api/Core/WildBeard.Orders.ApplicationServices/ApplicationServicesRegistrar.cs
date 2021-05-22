using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WildBeard.Orders.ApplicationServices.Mappers;
using WildBeard.Orders.ApplicationServices.RequestHandlers;
using WildBeard.Orders.ApplicationServices.Requests;
using WildBeard.Orders.ApplicationServices.RequestValidators;
using WildBeard.Orders.ApplicationServices.ResponseLinkGenerators;
using WildBeard.Orders.ApplicationServices.Responses;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices
{
    public static class ApplicationServicesRegistrar
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(PlaceNewOrderRequestHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // link generators
            services.AddScoped<ILinkGenerator<PlaceNewOrderResponse>, PlaceNewOrderLinkGenerator>();
            
            // hateos handlers
            services.AddScoped<IHateosResponseHandler<PlaceNewOrderResponse>, PlaceNewOrderHateosResponseHandler>();

            // mappers
            services.AddScoped<IRequestToDomainMapper<PlaceNewOrderRequest, Order>, OrderRequestToOrderDomainMapper>();

            services.AddScoped<IHttpContextProvider, HttpContextProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            //AppHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
