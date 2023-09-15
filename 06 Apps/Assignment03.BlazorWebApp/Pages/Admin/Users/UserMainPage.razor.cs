using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class UserMainPage
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

    public IList<User> WorkItemList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItemList = await HttpClientContext.User.GetListAllAsync(Model.AccessToken);

    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewDetail(string userId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Users/Details/{userId}");
    }

    private void AddNew()
    {
        this.NavigationManager.NavigateTo("/Admin/Users/New");
    }
    #endregion
}
