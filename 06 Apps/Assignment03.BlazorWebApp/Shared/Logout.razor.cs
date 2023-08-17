using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp;

public partial class Logout
{
    #region [ Properties - Inject]
    [Inject]
    private NavigationManager Navigation { get; set; }

    [Inject]
    private ISessionStorageService SessionStorage { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
        try {
            await SessionStorage.ClearAsync();

        } catch (Exception) {
            Navigation.NavigateTo("/", true);
            throw;
        }
        Navigation.NavigateTo("/", true);

        await base.OnInitializedAsync();
    }
    #endregion
}
