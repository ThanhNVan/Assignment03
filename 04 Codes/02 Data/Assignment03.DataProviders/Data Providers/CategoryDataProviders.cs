using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class CategoryDataProviders : BaseDataProvider<Category, AppDbContext>, ICategoryDataProviders
{
    #region [ CTor ]
    public CategoryDataProviders(ILogger<CategoryDataProviders> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
