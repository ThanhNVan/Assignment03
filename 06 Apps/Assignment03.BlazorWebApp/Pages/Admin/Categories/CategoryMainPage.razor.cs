using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class CategoryMainPage
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

    public IList<Category> WorkItemList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItemList = await HttpClientContext.Category.GetListAllAsync(Model.AccessToken);
    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewDetail(string CategoryId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Categories/Details/{CategoryId}");
    }

    private void AddNew()
    {
        this.NavigationManager.NavigateTo("/Admin/Categories/New");
    }
    #endregion
}
