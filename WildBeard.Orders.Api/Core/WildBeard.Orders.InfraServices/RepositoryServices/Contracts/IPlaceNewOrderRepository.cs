using System;
using System.Threading.Tasks;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices.RepositoryServices.Contracts
{
    public interface IPlaceNewOrderRepository
    {
        Task<Guid> PlaceNewOrderAsync(Order order);
    }
}
