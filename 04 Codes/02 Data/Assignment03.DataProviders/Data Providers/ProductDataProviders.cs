using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class ProductDataProviders : BaseDataProvider<Product, AppDbContext>, IProductDataProviders
{
    #region [ CTor ]
    public ProductDataProviders(ILogger<BaseDataProvider<Product, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
