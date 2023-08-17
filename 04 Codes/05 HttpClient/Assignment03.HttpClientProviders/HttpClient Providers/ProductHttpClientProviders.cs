using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class ProductHttpClientProviders : BaseHttpClientProvider<Product>, IProductHttpClientProviders
{
    #region [ CTor ]
    public ProductHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<Product>> logger) : base(httpClientFactory, logger) {
        this._entityUrl = EntityUrl.Product;
    }
    #endregion
}
