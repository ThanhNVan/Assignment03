using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class OrderDataProviders : BaseDataProvider<Order, AppDbContext>, IOrderDataProviders
{
    #region [ CTor ]
    public OrderDataProviders(ILogger<OrderDataProviders> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory)
    {
    }
    #endregion

    #region [ Methods - Checkout ]
    public async Task<string> CheckoutAsync(CheckoutModel model)
    {
        using var dbContext = await this.GetContextAsync();
        var userId = dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Email == model.Email).Id;
        var order = new Order();
        order.OrderDate = DateTime.UtcNow;
        order.RequiredDate = model.RequiredDate;
        order.ShippedDate = model.ShippedDate;
        order.Freight = model.Carts.Sum(x => x.Unit);
        order.UserId = userId;
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await dbContext.AddAsync(order);
                foreach (var item in model.Carts)
                {
                    var orderItem = new OrderItem();
                    orderItem.ProductId = item.ProductId;
                    orderItem.Quantity = item.Unit;
                    orderItem.OrderId = order.Id;

                    await dbContext.AddAsync(orderItem);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return order.Id;
            } catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                await transaction.RollbackAsync();
                return "";
            }
        });

        return order.Id;
    }
    #endregion
}
