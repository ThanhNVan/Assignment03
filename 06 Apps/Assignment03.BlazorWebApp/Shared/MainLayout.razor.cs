using Assignment03.EntityProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Assignment03.BlazorWebApp.Shared;

public partial class MainLayout
{
    #region [ Properties - Inject ]
    [Inject]
    private ISessionStorageService SessionStorage { get; set; }
    #endregion

    #region [ Properties ]
    private int Role { get; set; }

    private SignInSuccessModel Model { get; set; }
    #endregion

    #region [ Methods - Override ]
    protected override async Task OnInitializedAsync()
    {
        try
        {
            this.Model = await SessionStorage.GetItemAsync<SignInSuccessModel>(AppUserRole.Model);
            if (Model != null)
            {
                this.Role = Model.Role;
            } else
            {
                this.Role = -1;
            }

        } catch
        {
            this.Role = -1;
        }
    }
    #endregion
}
