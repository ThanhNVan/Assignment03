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
    private int Role { get; set; }

    private SignInSuccessModel Model { get; set; }

    public IList<Order> WorkItemList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
        try {
            this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);
            if (Model != null) {
                this.Role = Model.Role;
            } else {
                this.Role = -1;
            }

        } catch {
            this.Role = -1;
        }

        if (Role == (int)RoleEnums.Admin || Role == (int)RoleEnums.Manager) {
            this.WorkItemList = await HttpClientContext.Order.GetListAllAsync(Model.AccessToken);
            if (this.WorkItemList != null) {
                foreach (var item in this.WorkItemList) {
                    item.User = await HttpClientContext.User.GetSingleByIdAsync(item.UserId,Model.AccessToken);
                }
            }
        }
    }
    #endregion

    #region [ Private Methods -  ]
    private void ViewDetail(string orderId) {
        this.NavigationManager.NavigateTo($"/Admin/Orders/Details/{orderId}");
    }

    private void AddNew() {
        this.NavigationManager.NavigateTo("/Admin/Orders/New");
    }
    #endregion
}
