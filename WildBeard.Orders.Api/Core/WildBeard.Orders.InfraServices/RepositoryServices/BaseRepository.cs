using Microsoft.EntityFrameworkCore;
using System;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices.RepositoryServices
{
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = _context.Set<T>();
        }
    }
}
