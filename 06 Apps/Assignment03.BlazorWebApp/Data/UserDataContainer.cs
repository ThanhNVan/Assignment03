using Assignment03.EntityProviders;
using System;

namespace Assignment03.BlazorWebApp;

public class UserDataContainer
{
	#region [ Properties ]
	public SignInSuccessModel Model { get; set; }

    public event Action OnStateChange;

    public void SetValue(SignInSuccessModel value) {
        this.Model = value;
        NotifyStateChanged();
    }
    private void NotifyStateChanged() => OnStateChange?.Invoke();
    #endregion
}
