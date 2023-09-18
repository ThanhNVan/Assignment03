using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class ProductNewPage
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

    private SignInSuccessModel Model { get; set; }

    public Product WorkItem { get; set; }

    public IList<StringKeyValueModel> Categories { get; set; }

    public string SelectedCategoryId { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {

        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = new Product();
        this.WorkItem.Category = await HttpClientContext.Category.GetSingleByIdAsync(this.WorkItem.CategoryId, Model.AccessToken);
        this.Categories = (await this.HttpClientContext.Category.GetListAllAsync(Model.AccessToken))
                            .Select(x => new StringKeyValueModel { Key = x.Id, Value = x.Name }).ToList();
        this.SelectedCategoryId = Categories.FirstOrDefault().Key;
    }
    #endregion

    #region [ Methods - Private ]
    private async Task AddNewProductAsync()
    {
        var isValidInput = this.CheckInput();

        if (!isValidInput)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Not Valid Input");
            return;
        }

        this.WorkItem.CategoryId = this.SelectedCategoryId;
        var result = await this.HttpClientContext.Product.AddAsync(this.WorkItem, Model.AccessToken);
        if (result)
        {
            await JSRuntime.InvokeVoidAsync("alert", "Updated");
            this.NavigationManager.NavigateTo("/Admin/Products");
        }
    }

    private void ViewCateoryDetail(string categoryId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Categories/Details/{categoryId}");
    }

    private void OnChange(ChangeEventArgs<string, StringKeyValueModel> args)
    {
        this.SelectedCategoryId = args.ItemData.Key;
    }

    private bool CheckInput()
    {
        if (string.IsNullOrEmpty(this.WorkItem.Name) || string.IsNullOrEmpty(this.WorkItem.Weight))
        {
            return false;
        }

        if (0 >= this.WorkItem.Price || this.WorkItem.Price >= 10_000_000_000)
        {
            return false;
        }

        if (0 >= this.WorkItem.InStock || this.WorkItem.InStock >= 1_000_000)
        {
            return false;
        }

        return true;
    }
    #endregion
}
