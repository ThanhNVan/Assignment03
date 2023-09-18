using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class ProductDetailPage
{

    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }

    SfTextBox CategoryTextBox;

    [Inject]
    private IJSRuntime JSRuntime { get; set; }
    #endregion

    #region [ Properties ]
    [Parameter]
    public string Id { get; set; }

    private SignInSuccessModel Model { get; set; }

    public Product WorkItem { get; set; }

    public IList<StringKeyValueModel> Categories { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = await HttpClientContext.Product.GetSingleByIdAsync(Id, Model.AccessToken);
        this.WorkItem.Category = await HttpClientContext.Category.GetSingleByIdAsync(this.WorkItem.CategoryId, Model.AccessToken);
        this.Categories =( await this.HttpClientContext.Category.GetListAllAsync(Model.AccessToken))
                            .Select(x => new StringKeyValueModel { Key = x.Id, Value = x.Name}).ToList();
    }
    #endregion

    #region [ Methods - Private ]
    private async Task UpdateAsync()
    {
        var result = await this.HttpClientContext.Product.UpdateAsync(this.WorkItem, Model.AccessToken);
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
        var result = await this.HttpClientContext.Product.RecoverAsync(this.Id, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            await this.OnInitializedAsync();
        }
    }

    private async Task SoftDeleteAsync()
    {
        var result = await this.HttpClientContext.Product.SoftDeleteAsync(this.Id, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            await this.OnInitializedAsync();
        }
    }

    private async Task AddInfoIcon()
    {
        var click = EventCallback.Factory.Create<MouseEventArgs>(this, CategoryInfoClick);
        await this.CategoryTextBox.AddIconAsync("append", "e-icons e-circle-info", new Dictionary<string, object>() { { "onclick", click } });
    }

    private void CategoryInfoClick()
    {
        this.NavigationManager.NavigateTo($"/Admin/Categories/Details/{this.WorkItem.CategoryId}");
    }
    #endregion
}
