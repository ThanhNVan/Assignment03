using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class ProductDataProviders : BaseDataProvider<Product, AppDbContext>, IProductDataProviders
{
    #region [ CTor ]
    public ProductDataProviders(ILogger<ProductDataProviders> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion

    #region [ Methods - List ]
    public async Task<IList<Product>> GetListByOrderIdAsync(string categoryId)
    {
        try
        {
            using var context = await this.GetContextAsync();
            var result = await context.Products.AsNoTracking().Where(x => x.IsDeleted == false &&
                                                                        x.CategoryId == categoryId)
                                                    .OrderByDescending(x => x.LastUpdatedAt).ToListAsync();
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return null;
        }
    }
    #endregion
}
