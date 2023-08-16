using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class ProductLogicProviders : BaseLogicProvider<Product, IProductDataProviders>, IProductLogicProviders
{
    #region [ CTor ]
    public ProductLogicProviders(ILogger<BaseLogicProvider<Product, IProductDataProviders>> logger, IProductDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
