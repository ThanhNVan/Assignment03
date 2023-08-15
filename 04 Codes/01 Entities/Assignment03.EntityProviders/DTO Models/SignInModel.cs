using System.ComponentModel.DataAnnotations;

namespace Assignment03.EntityProviders;

public class SignInModel
{
    #region [ Properties ]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    #endregion

    #region [ CTor ]
    public SignInModel(string email, string password) {
        this.Email = email;
        this.Password = password;
    }

    public SignInModel() {

    }
    #endregion
}
