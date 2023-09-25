using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace Assignment03.BlazorWebApp;

public partial class CartMainPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private ICartLocalStorageService CartLocalStorageService { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }

    [Inject]
    private IJSRuntime JSRuntime { get; set; }
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public IList<Cart> CartList { get; set; }

    public ObservableCollection<ProductCart> WorkItems { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.CartList = await this.CartLocalStorageService.GetListAllAsync(Model.Email);
        
        this.WorkItems = new ObservableCollection<ProductCart>(await this.GetProductCartAsync());
    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewProductDetail(string productId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Products/Details/{productId}");
        return;
    }

    private async Task AddProductToCartAsync(string productId)
    {
        var productCart = this.WorkItems.FirstOrDefault(x => x.Id == productId);
        if (productCart.Unit == productCart.InStock)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Reach Maximum Number");
            return;
        }
        this.WorkItems.FirstOrDefault(x => x.Id == productId).Unit++;
        await this.CartLocalStorageService.AddProductToCartAsync(Model.Email, productId);
    }

    private async Task ReduceProductFromCartAsync(string productId)
    {
        var productCart = this.WorkItems.FirstOrDefault(x => x.Id == productId);
        if (productCart.Unit == 1)
        {
            this.WorkItems.Remove(productCart);
            return;
        }
        this.WorkItems.FirstOrDefault(x => x.Id == productId).Unit--;
        await this.CartLocalStorageService.ReduceProductFromCartAsync(Model.Email, productId);
    }

    private async Task<IEnumerable<ProductCart>> GetProductCartAsync()
    {
        var idList = this.CartList.Select(x => x.ProductId).ToList();

        var productInfoList = await this.HttpClientContext.Product.GetListByBatchAsync(idList, Model.AccessToken);
        return CartList.Join(productInfoList,
                                        cart => cart.ProductId,
                                        productInfo => productInfo.Id,
                                        (cart, productInfo) => new ProductCart()
                                        {
                                            Id = productInfo.Id,
                                            Name = productInfo.Name,
                                            Weight = productInfo.Weight,
                                            Price = productInfo.Price,
                                            InStock = productInfo.InStock,
                                            Category = productInfo.Category,
                                            Unit = cart.Unit,
                                        });

    }
    #endregion
}
