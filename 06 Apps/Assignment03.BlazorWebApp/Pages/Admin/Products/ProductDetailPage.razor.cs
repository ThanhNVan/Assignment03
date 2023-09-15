using Assignment03.EntityProviders;
using Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
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
    #endregion

    #region [ Properties ]
    [Parameter]
    public string Id { get; set; }

    private int Role { get; set; }

    private SignInSuccessModel Model { get; set; }

    public Product WorkItem { get; set; }

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

        if (Role == (int)RoleEnums.Admin || Role == (int)RoleEnums.Manager)
        {
            this.WorkItem = await HttpClientContext.Product.GetSingleByIdAsync(Id, Model.AccessToken);
            this.WorkItem.Category = await HttpClientContext.Category.GetSingleByIdAsync(this.WorkItem.CategoryId, Model.AccessToken);
        }
    }
    #endregion
}
