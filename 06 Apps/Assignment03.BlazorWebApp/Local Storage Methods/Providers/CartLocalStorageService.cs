using Assignment03.EntityProviders;
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public class CartLocalStorageService : ICartLocalStorageService
{
    #region [ Fields ]
    private readonly ILocalStorageService _localStorageService;
    #endregion

    #region [ CTor ]
    public CartLocalStorageService(ILocalStorageService localStorageService)
    {
        this._localStorageService = localStorageService;
    }
    #endregion

    #region [ Methods - Cart ]
    public async Task AddProductToCartAsync(string name, string productId)
    {
        var cartList = await this.GetListAllAsync(name);
        await this.AddProductToCartAsync(cartList,name,productId);
    }

    public async Task AddProductToCartAsync(IList<Cart> cartList, string name, string productId)
    {
        var cartEntity = cartList.FirstOrDefault(x => x.ProductId == productId);
        if (cartEntity == null)
        {
            var newCart = new Cart();
            newCart.ProductId = productId;
            newCart.Unit = 1;
            cartList.Add(newCart);
            await this._localStorageService.SetItemAsync(name, cartList);
            return;
        }
        cartList.FirstOrDefault(x => x.ProductId == productId).Unit++;
        await this._localStorageService.SetItemAsync(name, cartList);
        return;
    }

    public async Task ReduceProductFromCartAsync(string name, string productId)
    {
        var cartList = await GetListAllAsync(name);
        await this.ReduceProductFromCartAsync(cartList, name, productId);
    }

    public async Task ReduceProductFromCartAsync(IList<Cart> cartList, string name, string productId)
    {
        var cartEntity = cartList.FirstOrDefault(x => x.ProductId == productId);

        if (cartEntity == null)
        {
            return;
        }

        if (cartEntity.Unit == 1)
        {
            cartList.Remove(cartEntity);
            await this._localStorageService.SetItemAsync(name, cartList);
            return;
        }

        cartList.FirstOrDefault(x => x.ProductId == productId).Unit--;
        await this._localStorageService.SetItemAsync(name, cartList);
        return;
    }

    public async Task<IList<Cart>> GetListAllAsync(string name)
    {
        var result = await this._localStorageService.GetItemAsync<List<Cart>>(name);

        return result;
    }

    public async Task SetListAsync(string name, IList<Cart> cartList) {
        await this._localStorageService.SetItemAsync(name, cartList);
        return;
    }
    #endregion
}
