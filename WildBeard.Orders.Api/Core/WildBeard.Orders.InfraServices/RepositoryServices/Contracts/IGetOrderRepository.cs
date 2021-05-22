using System;
using System.Threading.Tasks;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices.RepositoryServices.Contracts
{
    public interface IGetOrderRepository
    {
        Task<Order> GetAsync(Guid id);
    }
}
