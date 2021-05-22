using System;
using System.Threading.Tasks;
using WildBeard.Orders.InfraServices.RepositoryServices.Contracts;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices.RepositoryServices
{
    public class PlaceNewOrderRepository : BaseRepository<Order>, IPlaceNewOrderRepository
    {
        public PlaceNewOrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Guid> PlaceNewOrderAsync(Order order)
        {
            await _dbSet.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
