using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class OrderMainPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public IList<Order> WorkItemList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItemList = await HttpClientContext.Order.GetListAllAsync(Model.AccessToken);
        if (this.WorkItemList != null)
        {
            foreach (var item in this.WorkItemList)
            {
                item.User = await HttpClientContext.User.GetSingleByIdAsync(item.UserId, Model.AccessToken);
            }
        }
    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewDetail(string orderId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Orders/Details/{orderId}");
    }

    private void AddNew()
    {
        this.NavigationManager.NavigateTo("/Admin/Orders/New");
    }
    #endregion
}
