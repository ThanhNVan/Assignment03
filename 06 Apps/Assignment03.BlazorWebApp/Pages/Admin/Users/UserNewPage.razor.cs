using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Assignment03.BlazorWebApp;

public partial class UserNewPage
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

    SfTextBox UserPhoneTextBox;
    #endregion

    #region [ Properties ]
    private SignInSuccessModel Model { get; set; }

    public User WorkItem { get; set; }

    public IList<IntKeyValueModel> UserPhones { get; set; }

    public IList<IntKeyValueModel> RoleList { get; set; }

    public int SelectedRole { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = new User();
        this.WorkItem.PasswordHash = "123456";
        this.UserPhones = new List<IntKeyValueModel>() 
        { new IntKeyValueModel() { Key = 1, Value = ""}
        };
        this.RoleList = Enum.GetValues(typeof(RoleEnums)).Cast<RoleEnums>()
           .Select(x => new IntKeyValueModel { Key = (int)x, Value = x.ToString() }).ToList();
        this.SelectedRole = RoleList.FirstOrDefault().Key;
    }
    #endregion

    #region [ Methods - Private ]
    private async Task AddNewUserAsync()
    {
        var isValidInput = await this.CheckInputAsync();

        if (!isValidInput)
        {
            return;
        }
        this.WorkItem.Role = this.SelectedRole;

        var model = new NewUserModel()
        {
            User = this.WorkItem,
            UserPhones = this.UserPhones.Select(x => x.Value).ToList(),
        };

        var result = await this.HttpClientContext.User.AddNewUserAsync(model, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            this.NavigationManager.NavigateTo("/Admin/Users");
        }
    }

    private async Task<bool> CheckInputAsync()
    {
        if (string.IsNullOrEmpty(this.WorkItem.Firstname))
        {
            await JSRuntime.InvokeVoidAsync("alert", "Not Valid Input");
            return false;
        }

        if (string.IsNullOrEmpty(this.WorkItem.Lastname))
        {
            await JSRuntime.InvokeVoidAsync("alert", "Not Valid Input");
            return false;
        }
        
        if (string.IsNullOrEmpty(this.WorkItem.Email))
        {
            await JSRuntime.InvokeVoidAsync("alert", "Not Valid Input");
            return false;
        }

        var isDuplicatedEmail = await HttpClientContext.User.IsDuplicatedEmailAsync(this.WorkItem.Email, Model.AccessToken);
        if (isDuplicatedEmail)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Duplicated Email");
            return false;
        }

        var isValidPhone = this.UserPhones.Any(x => string.IsNullOrEmpty(x.Value)); 
        if (isValidPhone)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Phone number cannot be null");
            return false;
        }

        var isDuplicatedPhone = this.UserPhones.GroupBy(x => x.Value).Where(group => group.Count() > 1).Any();
        if (isDuplicatedPhone)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Phone number cannot be duplicated");
            return false;
        }

        return true;
    }

    private void AddNewUserPhone()
    {
        this.UserPhones.Add(new IntKeyValueModel() { Key = 1, Value = "" });
        return;
    }

    private void RemoveUserPhone(string phoneNumber)
    {
        if (this.UserPhones.Count == 1)
        {
            return;
        }
        var entity = this.UserPhones.FirstOrDefault(x => x.Value == phoneNumber);

        this.UserPhones.Remove(entity);
        return;
    }

    private async Task CancelAsync()
    {
        await OnInitializedAsync();
        return;
    }
    #endregion
}
