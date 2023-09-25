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
    private ICartLocalStorageService CartLocalStorageService { get; set; }
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public IList<Product> WorkItemList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);
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
        await this.CartLocalStorageService.AddProductToCartAsync(Model.Email, productId);
    }

    private async Task ReduceProductFromCartAsync(string productId)
    {
        await this.CartLocalStorageService.ReduceProductFromCartAsync(Model.Email, productId);
    }
    #endregion
}
