using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp.Pages;

public partial class Index
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
    private string Email { get; set; }
    private string Password { get; set; }
    private string Warning { get; set; } = string.Empty;
    #endregion

    #region [ Methods - Public ]
    public async Task SignInAsync() {
        var isValid = CheckValidInput();
        if (!isValid) {
            return;
        }

        var signInModel = new SignInModel(Email, Password);

        var result = await this.HttpClientContext.User.SignInAsync(signInModel);

        if (result != null) {
            if (result.Model != null) {
                await SessionStorage.SetItemAsync(AppUserRole.Model, result.Model);
                await SessionStorage.SetItemAsync(AppUserRole.Role, result.Model.Role);

                NavigationManager.NavigateTo("Admin/Products", true);
            }
            this.Warning = "Incorrect Email or Password";
            return;
        }

        this.Warning = "Incorrect Email or Password";
        return;
    }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync() {
        var model = default(SignInSuccessModel);
        try {
            model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);

        } catch { }

        if (model == null) {
            return;
        }

        if (model.Role == (int)RoleEnums.Admin || model.Role == (int)RoleEnums.Manager) {
            NavigationManager.NavigateTo("Admin/Products", true);
        }

        await base.OnInitializedAsync();
    }
    #endregion

    #region [ Methods - Private ]
    private bool CheckValidInput() {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) {
            this.Warning = "Invalid Input";
            return false;
        }
        return true;
    }
    #endregion
}
