﻿using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class CategoryDetailPage
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

    private SignInSuccessModel Model { get; set; }

    public Category WorkItem { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);
        this.WorkItem = await HttpClientContext.Category.GetSingleByIdAsync(Id, Model.AccessToken);
        if (this.WorkItem != null)
        {
            this.WorkItem.Products = await HttpClientContext.Product.GetListByCategoryIdAsync(this.WorkItem.Id, Model.AccessToken);
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

    private void ViewProductDetail(string productId)
    {
        this.NavigationManager.NavigateTo($"/Admin/Products/Details/{productId}");
    }
    #endregion
}
