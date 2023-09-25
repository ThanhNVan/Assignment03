using Assignment03.EntityProviders;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public interface IProductLogicProviders : IBaseLogicProvider<Product>
{
    #region [ Methods - List ]
    Task<IList<Product>> GetListByOrderIdAsync(string categoryId);

    Task<IList<ProductInfoModel>> GetListByBatchAsync(IList<string> idList);
    #endregion
}
