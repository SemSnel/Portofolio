using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Orders;
using SemSnel.Portofolio.Infrastructure.Common.Persistence;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts.Orders;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(IAppDatabaseContext context, IMapper mapper) : base(context, mapper)
    {
    }
}

public interface IOrderRepository : IWriteRepository<Order, Guid>, IReadRepository<Order, Guid>
{
}