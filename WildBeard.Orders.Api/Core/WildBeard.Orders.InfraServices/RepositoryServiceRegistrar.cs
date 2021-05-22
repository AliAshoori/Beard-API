using Microsoft.Extensions.DependencyInjection;
using WildBeard.Orders.InfraServices.RepositoryServices;
using WildBeard.Orders.InfraServices.RepositoryServices.Contracts;

namespace WildBeard.Orders.InfraServices
{
    public static class RepositoryServiceRegistrar
    {
        public static void RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IPlaceNewOrderRepository, PlaceNewOrderRepository>();
        }
    }
}
