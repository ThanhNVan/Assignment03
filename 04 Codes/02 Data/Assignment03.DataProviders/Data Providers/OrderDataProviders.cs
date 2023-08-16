using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class OrderDataProviders : BaseDataProvider<Order, AppDbContext>, IOrderDataProviders
{
    #region [ CTor ]
    public OrderDataProviders(ILogger<BaseDataProvider<Order, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
