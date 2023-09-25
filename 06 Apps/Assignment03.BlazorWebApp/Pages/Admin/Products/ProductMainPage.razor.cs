using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class ProductMainPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; }
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public IList<Product> WorkItemList { get; set; }

    public IList<Cart> CartList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);
        this.CartList = await this.LocalStorageService.GetItemAsync<List<Cart>>(Model.Email);
        if (CartList == null)
        {
            this.CartList = new List<Cart>() { };
        }
        this.WorkItemList = await HttpClientContext.Product.GetListAllAsync(Model.AccessToken);
        if (this.WorkItemList != null)
        {

            var categoryList = await HttpClientContext.Category.GetListAllAsync(Model.AccessToken);
            foreach (var item in WorkItemList)
            {
                item.Category = categoryList.FirstOrDefault(x => x.Id == item.CategoryId);
            }
        }

    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewDetail(string productId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Products/Details/{productId}");
    }

    private void AddNew()
    {
        this.NavigationManager.NavigateTo("/Admin/Products/New");
    }
    private async Task AddProductToCartAsync(string productId)
    {
        var cartEntity = this.CartList.FirstOrDefault(x => x.ProductId == productId);
        if (cartEntity == null)
        {
            var newCart = new Cart();
            newCart.ProductId = productId;
            newCart.Unit = 1;
            this.CartList.Add(newCart);
            await this.LocalStorageService.SetItemAsync(Model.Email, this.CartList);
            return;
        }
        this.CartList.FirstOrDefault(x => x.ProductId == productId).Unit++;
        await this.LocalStorageService.SetItemAsync(Model.Email, this.CartList);
        return;
    }

    private async Task ReduceProductFromCartAsync(string productId)
    {
        var cartEntity = this.CartList.FirstOrDefault(x => x.ProductId == productId);
        if (cartEntity.Unit == 1)
        {
            this.CartList.Remove(cartEntity);
            await this.LocalStorageService.SetItemAsync(Model.Email, this.CartList);
            return;
        }

        this.CartList.FirstOrDefault(x => x.ProductId == productId).Unit--;
        await this.LocalStorageService.SetItemAsync(Model.Email, this.CartList);
        return;
    }
    #endregion
}
