using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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
    private ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public IList<Cart> CartList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.CartList = await this.LocalStorageService.GetItemAsync<List<Cart>>(Model.Email);
    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewProductDetail(string productId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Products/Details/{productId}");
        return;
    }

   
    #endregion
}
