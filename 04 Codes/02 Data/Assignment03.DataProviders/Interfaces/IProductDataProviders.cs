using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IProductDataProviders: IBaseDataProvider<Product>
{
    #region [ Methods - List ]
    Task<IList<Product>> GetListByOrderIdAsync(string categoryId);

    Task<IList<ProductInfoModel>> GetListByBatchAsync(IList<string> idList);
    #endregion
}
