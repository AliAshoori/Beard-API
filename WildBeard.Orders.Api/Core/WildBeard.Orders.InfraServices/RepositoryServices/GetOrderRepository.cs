using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WildBeard.Orders.InfraServices.RepositoryServices.Contracts;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices.RepositoryServices
{
    public class GetOrderRepository : BaseRepository<Order>, IGetOrderRepository
    {
        public GetOrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order> GetAsync(Guid id)
        {
            var data = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

            return data;
        }
    }
}
