using Assignment03.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IProductHttpClientProviders : IBaseHttpClientProvider<Product>
{
    #region [ Methods - List ]
    Task<IList<Product>> GetListByCategoryIdAsync(string categoryId, string accessToken = "");
    #endregion
}
