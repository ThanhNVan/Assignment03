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

    public async Task<IList<ProductInfoModel>> GetListByBatchAsync(IList<string> idList)
    {
        try
        {
            using var context = await this.GetContextAsync();
            var result = await context.Products.AsNoTracking()
                                .Where(x => idList.Contains(x.Id))
                                .Join(context.Categories.AsNoTracking(),
                                    product => product.CategoryId,
                                    category => category.Id,
                                    (product, category) => new ProductInfoModel
                                    { 
                                        Id = product.Id,
                                        Name = product.Name,
                                        Weight = product.Weight,
                                        Price = product.Price,
                                        InStock = product.InStock,
                                        Category = category.Name
                                    })
                                .ToListAsync();

            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return null;
        }
    }
    #endregion
}
