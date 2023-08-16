using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class OrderItemDataProviders : BaseDataProvider<OrderItem, AppDbContext>, IOrderItemDataProviders
{
    #region [ CTor ]
    public OrderItemDataProviders(ILogger<BaseDataProvider<OrderItem, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
