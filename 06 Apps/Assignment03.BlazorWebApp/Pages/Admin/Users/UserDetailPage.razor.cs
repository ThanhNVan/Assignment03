using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class UserDetailPage
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
    [Parameter]
    public string Id { get; set; }

    private int Role { get; set; }

    private SignInSuccessModel Model { get; set; }

    public User WorkItem { get; set; }

    public IList<KeyValueModel> RoleList { get; set; }

    public IList<UserPhone> UserPhoneList { get; set; }
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
            this.WorkItem = await HttpClientContext.User.GetSingleByIdAsync(Id, Model.AccessToken);
            this.RoleList = Enum.GetValues(typeof(RoleEnums)).Cast<RoleEnums>()
                .Select( x => new KeyValueModel { Key = (int)x,  Value = x.ToString()}).ToList();
            this.UserPhoneList = await this.HttpClientContext.UserPhone.GetListByUserIdAsync(this.Id,Model.AccessToken);
        }
    }
    #endregion
}
