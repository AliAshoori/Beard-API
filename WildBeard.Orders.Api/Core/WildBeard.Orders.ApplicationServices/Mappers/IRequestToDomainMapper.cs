using WildBeard.Orders.Model;

namespace WildBeard.Orders.ApplicationServices.Mappers
{
    public interface IRequestToDomainMapper<TRequest, TDomain> 
        where TRequest : class
        where TDomain : BaseEntity
    {
        TDomain Map(TRequest request);
    }
}
