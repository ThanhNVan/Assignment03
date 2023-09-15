namespace Assignment03.EntityProviders;

public class SignInResponseModel
{
    #region [ Properties ]
    public bool Success { get; set; }

    public string Message { get; set; }

    public SignInSuccessModel Model { get; set; }
    #endregion
}
