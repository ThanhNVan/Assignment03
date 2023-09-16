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

    #region [ Methods - List ]
    public async Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId)
    {
        var result = default(IList<OrderItem>);

        try
        {
            using var context = await this.GetContextAsync();
            result = await context.OrderItems.AsNoTracking().Where(x => x.IsDeleted == false &&
                                                                    x.OrderId == orderId)
                                        .OrderByDescending(x => x.LastUpdatedAt).ToListAsync();

            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
