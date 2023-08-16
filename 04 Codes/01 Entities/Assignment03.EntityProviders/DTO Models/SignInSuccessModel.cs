namespace Assignment03.EntityProviders;

public class SignInSuccessModel
{
	#region [ Properties ]
	public string Email { get; set; }

	public string Fullname { get; set; }

	public int Role { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
    #endregion
}
