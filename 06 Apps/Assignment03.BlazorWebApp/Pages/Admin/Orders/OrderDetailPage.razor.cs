using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Maps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class OrderDetailPage
{
    #region [ Properties - Inject ]
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }

    [Inject]
    private HttpClientContext HttpClientContext { get; set; }

    SfTextBox UserEmailTextBox;
    #endregion

    #region [ Properties ]
    [Parameter]
    public string Id { get; set; }

    private SignInSuccessModel Model { get; set; }

    public Order WorkItem { get; set; }

    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        this.WorkItem = await HttpClientContext.Order.GetSingleByIdAsync(Id, Model.AccessToken);
        this.WorkItem.User = await HttpClientContext.User.GetSingleByIdAsync(this.WorkItem.UserId, Model.AccessToken);
        this.WorkItem.OrderItems = await HttpClientContext.OrderItem.GetListByOrderIdAsync(this.WorkItem.Id, Model.AccessToken);
        foreach (var item in this.WorkItem.OrderItems)
        {
            item.Product = await this.HttpClientContext.Product.GetSingleByIdAsync(item.ProductId, Model.AccessToken);
        }
    }
    #endregion

    #region [ Methods - Private ]
    private async Task UpdateAsync()
    {

    }

    private async Task CancelAsync()
    {
        await this.OnInitializedAsync();
    }

    private async Task RecoverAsync()
    {

    }

    private async Task SoftDeleteAsync()
    {

    }

    private async Task AddInfoIcon()
    {
        var click = EventCallback.Factory.Create<MouseEventArgs>(this, UserInfoClick);
        await this.UserEmailTextBox.AddIconAsync("append", "e-icons e-circle-info", new Dictionary<string, object>() { { "onclick", click } });
    }

    private void UserInfoClick()
    {
        this.NavigationManager.NavigateTo($"/Admin/Users/Details/{this.WorkItem.UserId}");
    }

    private void ViewProductDetail(string productId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Products/Details/{productId}");
    }
    #endregion
}
