using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class ProductLogicProviders : BaseLogicProvider<Product, IProductDataProviders>, IProductLogicProviders
{
    #region [ CTor ]
    public ProductLogicProviders(ILogger<ProductLogicProviders> logger, IProductDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
    #region [ Methods - List ]
    public async Task<IList<Product>> GetListByOrderIdAsync(string categoryId){
        if (string.IsNullOrEmpty(categoryId))
        {
            throw new ArgumentNullException(nameof(categoryId));
        }

        return await this._dataProvider.GetListByOrderIdAsync(categoryId);
    }
    public async Task<IList<ProductInfoModel>> GetListByBatchAsync(IList<string> idList)
    {
        if (idList.Where(x => string.IsNullOrEmpty(x)).Any())
        {
            throw new ArgumentNullException();
        }

        return await this._dataProvider.GetListByBatchAsync(idList);
    }
    #endregion
}
