using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

    [Inject]
    private IJSRuntime JSRuntime { get; set; }
    #endregion

    #region [ Properties ]
    [Parameter]
    public string Id { get; set; }

    private int Role { get; set; }

    private SignInSuccessModel Model { get; set; }

    public User WorkItem { get; set; }

    public IList<IntKeyValueModel> RoleList { get; set; }

    public IList<UserPhone> UserPhoneList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = await HttpClientContext.User.GetSingleByIdAsync(Id, Model.AccessToken);
        this.RoleList = Enum.GetValues(typeof(RoleEnums)).Cast<RoleEnums>()
            .Select(x => new IntKeyValueModel { Key = (int)x, Value = x.ToString() }).ToList();
        this.UserPhoneList = await this.HttpClientContext.UserPhone.GetListByUserIdAsync(this.Id, Model.AccessToken);

    }
    #endregion

    #region [ Methods - Private ]
    private async Task UpdateAsync() {
        var result = await this.HttpClientContext.User.UpdateAsync(this.WorkItem, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            await this.OnInitializedAsync();
        }
    }

    private async Task CancelAsync()
    {
        await this.OnInitializedAsync();
    }

    private async Task RecoverAsync()
    {
        var result = await this.HttpClientContext.User.RecoverAsync(this.Id, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            await this.OnInitializedAsync();
        }
    }

    private async Task SoftDeleteAsync()
    {
        var result = await this.HttpClientContext.User.SoftDeleteAsync(this.Id, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            await this.OnInitializedAsync();
        }
    }
    #endregion
}
