using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class CategoryNewPage
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
    private SignInSuccessModel Model { get; set; }

    public Category WorkItem { get; set; }

    public IList<Category> CategoryList { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = new Category();

        this.CategoryList = await this.HttpClientContext.Category.GetListAllAsync(Model.AccessToken);
    }
    #endregion

    #region [ Methods - Private ]
    private async Task AddNewCategoryAsync()
    {
        var isValidInput = await this.CheckInputAsync();

        if (!isValidInput)
        {
            return;
        }

        var result = await this.HttpClientContext.Category.AddAsync(this.WorkItem, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            this.NavigationManager.NavigateTo("/Admin/Categories");
        }
    }

    private async Task<bool> CheckInputAsync()
    {
        if (string.IsNullOrEmpty(this.WorkItem.Name))
        {
            await JSRuntime.InvokeVoidAsync("alert", "Not Valid Input");
            return false;
        }

        var isDuplicatedName = this.CategoryList.Where(x => x.Name.Equals(this.WorkItem.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        if (isDuplicatedName)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Duplicated Name");
            return false;
        }

        return true;
    }

    private async Task CancelAsync()
    {
        await this.OnInitializedAsync();
    }
    #endregion
}
