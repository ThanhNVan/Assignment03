using Assignment03.EntityProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public interface ICartLocalStorageService
{
    #region [ Methods - Cart ]
    Task AddProductToCartAsync(string name,string productId);
    Task AddProductToCartAsync(IList<Cart> cartList, string name,string productId);
    Task ReduceProductFromCartAsync(string name,string productId);
    Task ReduceProductFromCartAsync(IList<Cart> cartList, string name,string productId);

    Task<IList<Cart>> GetListAllAsync(string name);
    #endregion
}
