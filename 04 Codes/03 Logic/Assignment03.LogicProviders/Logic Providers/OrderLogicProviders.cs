using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class OrderLogicProviders : BaseLogicProvider<Order, IOrderDataProviders>, IOrderLogicProviders
{
    #region [ CTor ]
    public OrderLogicProviders(ILogger<OrderLogicProviders> logger, IOrderDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods - Checkout ]
    public async Task<string> CheckoutAsync(CheckoutModel model)
    {
        if (string.IsNullOrEmpty(model.Email))
        {
            this._logger.LogWarning($"Null {nameof(model.Email)}");
            throw new ArgumentNullException();
        }

        if (model.Carts == null)
        {
            this._logger.LogWarning($"Null {nameof(model.Carts)}");
            throw new ArgumentNullException();
        }
        
        if (model.Carts.Where(x => string.IsNullOrEmpty(x.ProductId)).Any())
        {
            this._logger.LogWarning($"Null {nameof(model.Carts)}");
            throw new ArgumentNullException();
        }
        
        if (model.Carts.Where(x => x.Unit <= 0).Any())
        {
            this._logger.LogWarning($"Null {nameof(model.Carts)}");
            throw new ArgumentNullException();
        }

        return await this._dataProvider.CheckoutAsync(model);
    }
    #endregion
}
